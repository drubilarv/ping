using System.Net.NetworkInformation;
using Apex.MVVM;
using Network;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Ping = Network.Ping;
using Ping.Accion;
using System.Collections.Generic;

namespace PingWpf.ViewModels
{
    class PingTaskViewModel : ViewModelBase
    {

        private string hostName = "<not set>";
        public string HostName
        {
            get { return hostName; }
            set
            {
                if (hostName == value) return;
                hostName = value;
                RaisePropertyChanged();
            }
        }
        private string address = "<not set>";
        public string Address
        {
            get { return address; }
            set
            {
                if (address == value) return;
                address = value;
                RaisePropertyChanged();
            }
        }
        private string res = String.Empty;
        public string Res
        {
            get { return res; }
            set
            {
                if (res == value) return;
                res = value;
                RaisePropertyChanged();
            }
        }
        private string averageTime = "<na>";
        public string AverageTime
        {
            get { return averageTime; }
            set
            {
                if (averageTime == value) return;
                averageTime = value;
                RaisePropertyChanged();
            }
        }
        private string lastTime = "<na>";
        public string LastTime
        {
            get { return lastTime; }
            set
            {
                if (lastTime == value) return;
                lastTime = value;
                RaisePropertyChanged();
            }
        }
        private bool? lastPingSuccessfull;
        public bool? LastPingSuccessfull
        {
            get { return lastPingSuccessfull; }
            set
            {
                if (lastPingSuccessfull == value) return;
                lastPingSuccessfull = value;
                RaisePropertyChanged();
            }
        }
        private string successRate = FormatSuccessRate(0, 0);
        public string SuccessRate
        {
            get { return successRate; }
            set
            {
                if (successRate == value) return;
                successRate = value;
                RaisePropertyChanged();
            }
        }
        private int contadorPingFracaso;
        public int ContadorPingFracaso
        {
            get { return contadorPingFracaso; }
            set
            {
                if (contadorPingFracaso == value) return;
                contadorPingFracaso = value;
                RaisePropertyChanged();
            }
        }
        private int contadorPingExito;
        public int ContadorPingExito
        {
            get { return contadorPingExito; }
            set
            {
                if (contadorPingExito == value) return;
                contadorPingExito = value;
                RaisePropertyChanged();
            }
        }
        private DateTime timeAlerta100;
        public DateTime TimeAlerta100
        {
            get { return timeAlerta100; }
            set
            {
                if (timeAlerta100 == value) return;
                timeAlerta100 = value;
                RaisePropertyChanged();
            }
        }
        private bool? estadoPing100 = false;//  true= generado alerta 1 false= no generado alerta  null= generado alerta 2
        public bool? EstadoPing100
        {
            get { return estadoPing100; }
            set
            {
                if (estadoPing100 == value) return;
                estadoPing100 = value;
                RaisePropertyChanged();
            }
        }
        private DateTime timeAlertaDistinto100;
        public DateTime TimeAlertaDistinto100
        {
            get { return timeAlertaDistinto100; }
            set
            {
                if (timeAlertaDistinto100 == value) return;
                timeAlertaDistinto100 = value;
                RaisePropertyChanged();
            }
        }
        private bool? estadoPingDistinto100 = false;//  true= generado alerta 1 false= no generado alerta  null= generado alerta 2
        public bool? EstadoPingDistinto100
        {
            get { return estadoPingDistinto100; }
            set
            {
                if (estadoPingDistinto100 == value) return;
                estadoPingDistinto100 = value;
                RaisePropertyChanged();
            }
        }
        public Command RemoveTask { get; private set; }
        private CancellationTokenSource Cts { get; set; }
        private DateTime _fechaTimeAux = DateTime.Now;
        static List<string> ips = new List<string>();
        private PingTaskViewModel(string hostName, string ipAddress, CancellationToken ct, Action<PingTaskViewModel> removeTask)
        {
            HostName = hostName;
            Cts = new CancellationTokenSource();

            RemoveTask = new Command(
                () =>
                {
                    if (ipAddress == null) return;
                    string ip = ipAddress;
                    string nameEquip = hostName;
                    var pantalla = new WindowsDetallePing(ip, nameEquip);
                    pantalla.Title = "Detalle Ping " + nameEquip + " (" + ip + ")";
                    pantalla.Show(); //.ShowDialog();
                    return;
                });
            var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct, Cts.Token);
            StartPing(ipAddress, linkedCts.Token);
        }

        public async void StartPing(string ipAddress, CancellationToken ct)
        {
            var progress = new Progress<PingResult>(HandleProgress);
            var configMonitAction = new ConfiguracionMonitoreo_action();
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    try
                    {
                        var idConfig =ConfiguracionMonitoreo_action.GetIdConfiguracion(ipAddress);//1
                        var parametros = configMonitAction.GetConfiguracionPorId(idConfig);//1
                        int timeout = Convert.ToInt32(parametros.Timeout.Contains(",") ? parametros.Timeout.Split(',')[0] : parametros.Timeout);//1000;
                        int timeEntrePing = Convert.ToInt32(parametros.TiempoPing.Contains(",") ? parametros.TiempoPing.Split(',')[0] : parametros.TiempoPing);//250
                        byte[] tamanoPaquete = BitConverter.GetBytes(Convert.ToDouble(parametros.TamPaquete));//BitConverter.GetBytes(Convert.ToDouble(parametros.TamPaquete));
                        var pingTask = Network.Ping.ContinuousPingAsync(ipAddress, timeout, timeEntrePing, tamanoPaquete, progress, ct, ips);
                        //var pingTask = Network.Ping.ContinuousPingAsync(ipAddress, 1000, 250, progress, ct);
                        await pingTask;
                    }
                    catch (PingException)
                    {
                        Address = "<unreachable>";
                        LastPingSuccessfull = null;
                    }
                    if (!ct.IsCancellationRequested) await Task.Delay(TimeSpan.FromSeconds(0));
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }
        public PingTaskViewModel()
        {
        }
        private void HandleProgress(PingResult res)
        {
            Address = res.Address.ToString();
            if (res.PingsSuccessfull > 0)
                AverageTime = FormatTime(res.AverageTime);
            LastPingSuccessfull = res.LastStatus == IPStatus.Success;
            SuccessRate = FormatSuccessRate(res.PingsSuccessfull, res.PingsTotal);
            Res = HostName + "      " + res.Address + "        " + res.TimeEntrePing + "    " + SuccessRate;//envia string por pantalla
            if (LastPingSuccessfull.Value)
            {
                LastTime = FormatTime(res.LastTime);
                contadorPingFracaso = 0;
                contadorPingExito++;
                estadoPing100 = false;
                estadoPingDistinto100 = false;
                if (ips != null && ips.Contains(Address))ips.Remove(Address);
                //if (contadorPingExito >= Convert.ToInt32(ConfigurationManager.AppSettings.Get("nroPingVerde")))
                //    Fondo = ConfigurationManager.AppSettings.Get("colorVerde");
            }
            else
            {
                LastTime = "<na>";
                contadorPingFracaso++;
                contadorPingExito = 0;
                //if (contadorPingFracaso >= Convert.ToInt32(ConfigurationManager.AppSettings.Get("nroPingRojo")))
                //    Fondo = ConfigurationManager.AppSettings.Get("colorRojo");
                
                var configuracionGeneralAction = new ConfiguracionGeneral_action();
                var tablaConfigGeneral = configuracionGeneralAction.ObtenerConfig();

                if (DateTime.Now.Subtract(_fechaTimeAux).Seconds < 40) return;//ingresa despues de 40 segundos iniciada la aplicación

                var monitoreoAction = new Monitoreo_action();
                var listaPing = monitoreoAction.GetMonitoreoPorIp(Address, (int)tablaConfigGeneral.Tiempo_nueva_alerta);
                if (listaPing.Count > 1)
                {
                    DateTime fechaActual = DateTime.Now;
                    var alertaMonitoreoAction = new AlertasMonitoreo_Action();
                    var listaPingNoExito = monitoreoAction.GetMonitoreoPingNoExitoPorIp(Address, (int)tablaConfigGeneral.Tiempo_nueva_alerta);

                    var porcentPingNoExitosos = tablaConfigGeneral.Ping_no_exitoso;
                    var porcentajeSeg = ((listaPingNoExito.Count * 100) / listaPing.Count);

                    bool? envioCorreo;
                    if (porcentajeSeg == 100) //100% ping no exitoso
                    {
                        var segundosEspera = (int)tablaConfigGeneral.Generar_alarma;
                        if (estadoPing100 == false) //sin alertas previas
                        {
                            envioCorreo = SendEmail_action.Send("Alerta, el equipo con ip: " + Address + " tiene un 100% de ping no exitosos en los ultimos " + segundosEspera + " segundos");
                            alertaMonitoreoAction.InsertAlertaMonitoreo(Address, fechaActual, 100, false);
                            timeAlerta100 = fechaActual;
                            estadoPing100 = true;
                            if (envioCorreo == false)
                                System.Windows.MessageBox.Show("Correo alerta no enviado, problemas con el envío de correo, comuníquese con el administrador", "Información", System.Windows.MessageBoxButton.OK);
                        }
                        else if (estadoPing100 == true)//alerta 1 generada previamente
                        {
                            if (timeAlerta100.AddSeconds(segundosEspera) < DateTime.Now)
                            {
                                envioCorreo = SendEmail_action.Send("Alerta," + Environment.NewLine + " el equipo con ip: " + Address + " tiene un 100% de ping no exitosos en los ultimos " + segundosEspera + " segundos, se desactivaran las alertas y se cambiara la frecuencia de ping. " + Environment.NewLine + " Las alertas se activaran luego que exista un ping exitoso.");
                                alertaMonitoreoAction.InsertAlertaMonitoreo(Address, fechaActual, 100, false);
                                estadoPing100 = null;

                                // y tomar otra frecuencia desde la tabla configuracion_general
                                ips.Add(Address);
                                //res.PingsSuccessfull = 0;
                                //res.PingsTotal = 0;
                                if (envioCorreo == false)
                                    System.Windows.MessageBox.Show("Correo alerta no enviado, problemas con el envío de correo, comuníquese con el administrador", "Información", System.Windows.MessageBoxButton.OK);
                            }
                        }
                    }
                    else if (porcentajeSeg > porcentPingNoExitosos && porcentajeSeg < 100)
                    {
                        var timeEsperaEnSegundos = (int)tablaConfigGeneral.Tiempo_nueva_alerta;
                        if (estadoPingDistinto100 == false) //sin alertas previas
                        {
                            alertaMonitoreoAction.InsertAlertaMonitoreo(Address, fechaActual, porcentajeSeg, false);
                            envioCorreo = SendEmail_action.Send("Alerta," + Environment.NewLine + " el equipo con ip: " + Address + "tiene un " + porcentajeSeg + "% de ping no exitosos en los ultimos " + timeEsperaEnSegundos + " segundos");
                            timeAlertaDistinto100 = fechaActual;
                            estadoPingDistinto100 = true;

                            if (envioCorreo == false)
                                System.Windows.MessageBox.Show("Correo alerta no enviado, problemas con el envío de correo, comuníquese con el administrador", "Información", System.Windows.MessageBoxButton.OK);
                        }
                        else if (estadoPingDistinto100 == true)//alerta 1 generada previamente
                        {
                            if (TimeAlertaDistinto100.AddSeconds(timeEsperaEnSegundos) < DateTime.Now)
                            {
                                alertaMonitoreoAction.InsertAlertaMonitoreo(Address, fechaActual, porcentajeSeg, false);
                                envioCorreo = SendEmail_action.Send("Alerta," + Environment.NewLine + " el equipo con ip: " + Address + " tiene un " + porcentajeSeg + "% de ping no exitosos en los ultimos " + timeEsperaEnSegundos + " segundos, se desactivaran las alertas hasta que exista un ping exitoso.");
                                estadoPingDistinto100 = null;

                                if (envioCorreo == false)
                                    System.Windows.MessageBox.Show("Correo alerta no enviado, problemas con el envío de correo, comuníquese con el administrador", "Información", System.Windows.MessageBoxButton.OK);
                            }
                        }
                    }
                }
            }
        }

        private string FormatTime(TimeSpan timeSpan)
        {
            return string.Format("{0:0} ms", timeSpan.TotalMilliseconds);
        }

        private static string FormatSuccessRate(int success, int total)
        {
            if (total == 0)
                return string.Format("{0}/{1}", success, total);
            else
            {
                var percent = (success * 100) / total;
                return string.Format("{0}/{1} ({2}%)", success, total, percent);
            }
        }
        public static async Task<PingTaskViewModel> CreateVmAsync(string ip,string name, CancellationToken ct, Action<PingTaskViewModel> removeTask)
        {
            return new PingTaskViewModel(name, ip, ct, removeTask);
        }
        //public static async Task<PingTaskViewModel> CleanVmAsync()
        //{
        //    var pingTask = new PingTaskViewModel();
        //    return pingTask;
        //}
    }
}
