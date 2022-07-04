using Ping.Accion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PingWpf
{
    /// <summary>
    /// Lógica de interacción para WindowsDetallePing.xaml
    /// </summary>
    public partial class WindowsDetallePing : Window
    {
        int? _conteo = 0;
        int? _contador = 0;
        int timeOut = 1000;
        private string cadena = "";
        System.Timers.Timer _tiempoUno = new System.Timers.Timer();
        System.Timers.Timer _tiempoDos = new System.Timers.Timer();
        System.Timers.Timer _tiempoTres = new System.Timers.Timer();
        System.Timers.Timer _tiempoCuatro = new System.Timers.Timer();
        System.Timers.Timer _tiempoCinco = new System.Timers.Timer();
        System.Timers.Timer _tiempoSeis = new System.Timers.Timer();
        public static class DatoIp
        {
            public static string ipAux1 { get; set; }
            public static string ipAux2 { get; set; }
            public static string ipAux3 { get; set; }
            public static string ipAux4 { get; set; }
            public static string ipAux5 { get; set; }
            public static string ipAux6 { get; set; }
            public static int nroEntradas { get; set; }
        }
        public WindowsDetallePing(string ip, string nombreEquipo)
        {
            try
            {
                InitializeComponent();
                _tiempoUno.Elapsed += new ElapsedEventHandler(Tick_1);
                _tiempoUno.Interval = 1000;
                _tiempoDos.Elapsed += new ElapsedEventHandler(Tick_2);
                _tiempoDos.Interval = 1000;
                _tiempoTres.Elapsed += new ElapsedEventHandler(Tick_3);
                _tiempoTres.Interval = 1000;
                _tiempoCuatro.Elapsed += new ElapsedEventHandler(Tick_4);
                _tiempoCuatro.Interval = 1000;
                _tiempoCinco.Elapsed += new ElapsedEventHandler(Tick_5);
                _tiempoCinco.Interval = 1000;
                _tiempoSeis.Elapsed += new ElapsedEventHandler(Tick_6);
                _tiempoSeis.Interval = 1000;
              
                DatoIp.nroEntradas++;
                if (DatoIp.nroEntradas == 1)
                {
                    DatoIp.ipAux1 = ip;
                    doPingDetal1(DatoIp.ipAux1);
                    ShowListBox1();
                }
                if (DatoIp.nroEntradas == 2)
                {
                    DatoIp.ipAux2 = ip;
                    doPingDetal2(DatoIp.ipAux2);
                    ShowListBox2();
                }
                if (DatoIp.nroEntradas == 3)
                {
                    DatoIp.ipAux3 = ip;
                    doPingDetal3(DatoIp.ipAux3);
                    ShowListBox3();
                }
                if (DatoIp.nroEntradas == 4)
                {
                    DatoIp.ipAux4 = ip;
                    doPingDetal4(DatoIp.ipAux4);
                    ShowListBox4();
                }
                if (DatoIp.nroEntradas == 5)
                {
                    DatoIp.ipAux5 = ip;
                    doPingDetal5(DatoIp.ipAux5);
                    ShowListBox5();
                }
                if (DatoIp.nroEntradas == 6)
                {
                    DatoIp.ipAux6 = ip;
                    doPingDetal6(DatoIp.ipAux6);
                    ShowListBox6();
                }
                if (DatoIp.nroEntradas > 6)
                {
                    MessageBox.Show(this, "6 es el limite de ventanas abiertas",
                        "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "WindowsDetallePing.xaml.cs(metodo WindowsDetallePing) " + ex.Message);
            }
        }
        private void doPingDetal1(string ip)
        {
            try
            {
                var Pings = new System.Net.NetworkInformation.Ping();
                var data = Pings.Send(ip, timeOut);

                if (data.Status != System.Net.NetworkInformation.IPStatus.Success)
                {
                    ltbDetalle1.Dispatcher.BeginInvoke(new System.Action(() => ltbDetalle1.Items.Add("Ping no exitoso! " + DateTime.Now)));
                    _tiempoUno.Start();
                    return;
                }

                _conteo = 0;
                _conteo++;
                _tiempoUno.Start();
                ltbDetalle1.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle1.Items.Add("Respuesta desde " + data.Address +
                                          ": bytes=" + data.Buffer.Length +
                                          " tiempo=" + data.RoundtripTime + "ms" +
                                          " TTL=" + data.Options.Ttl +
                                          "  hora=" + DateTime.Now);
                }));
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "WindowsDetallePing.xaml.cs(metodo doPingDetal1) " + ex.Message);
            }
        }

        private void Tick_1(object source, ElapsedEventArgs e)
        {
            try
            {
                var Pings = new System.Net.NetworkInformation.Ping();
                var data = Pings.Send(DatoIp.ipAux1, timeOut);

                _conteo++;
                _contador = _conteo;
                _tiempoUno.Start();

                if (data.Status != System.Net.NetworkInformation.IPStatus.Success)
                {
                    if (_contador < 11)
                        ltbDetalle1.Dispatcher.BeginInvoke(
                            new System.Action(() => ltbDetalle1.Items.Add("Ping no exitoso! " + DateTime.Now)));
                    else
                    {
                        ltbDetalle1.Dispatcher.BeginInvoke(new System.Action(() =>
                        {
                            ltbDetalle1.Items.Add("Ping no exitoso! " + DateTime.Now);
                            ltbDetalle1.Items.RemoveAt(0);
                        }));
                    }
                }
                else if (_contador >= 11)
                {
                    ltbDetalle1.Dispatcher.BeginInvoke(new System.Action(() =>
                    {
                        ltbDetalle1.Items.Add("Respuesta desde " + data.Address +
                                              ": bytes=" + data.Buffer.Length +
                                              " tiempo=" + data.RoundtripTime + "ms" +
                                              " TTL=" + data.Options.Ttl +
                                              "  hora=" + DateTime.Now);
                        ltbDetalle1.Items.RemoveAt(0);
                    }));
                }
                else
                {
                    ltbDetalle1.Dispatcher.BeginInvoke(new System.Action(() =>
                    {
                        ltbDetalle1.Items.Add("Respuesta desde " + data.Address +
                                              ": bytes=" + data.Buffer.Length +
                                              " tiempo=" + data.RoundtripTime + "ms" +
                                              " TTL=" + data.Options.Ttl +
                                              "  hora=" + DateTime.Now);
                    }));
                }
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "WindowsDetallePing.xaml.cs(metodo Tick_1) " + ex.Message);
            }
        }
        private void doPingDetal2(string ip)
        {
            try
            {
                var Pings = new System.Net.NetworkInformation.Ping();
                var data = Pings.Send(ip, timeOut);

                if (data.Status != System.Net.NetworkInformation.IPStatus.Success)
                {
                    ltbDetalle2.Dispatcher.BeginInvoke(new System.Action(() => ltbDetalle2.Items.Add("Ping no exitoso!")));
                    _tiempoDos.Start();
                    return;
                }

                _conteo = 0;
                _conteo++;
                _tiempoDos.Start();
                ltbDetalle2.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle2.Items.Add("Respuesta desde " + data.Address +
                                          ": bytes=" + data.Buffer.Length +
                                          " tiempo=" + data.RoundtripTime + "ms" +
                                          " TTL=" + data.Options.Ttl +
                                          "  hora=" + DateTime.Now);
                }));
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "WindowsDetallePing.xaml.cs(metodo doPingDetal2) " + ex.Message);
            }
        }
        private void Tick_2(object source, ElapsedEventArgs e)
        {
            try
            {
                var Pings = new System.Net.NetworkInformation.Ping();
                var data = Pings.Send(DatoIp.ipAux2, timeOut);

                _conteo++;
                _contador = _conteo;
                _tiempoDos.Start();

                if (data.Status != System.Net.NetworkInformation.IPStatus.Success)
                {
                    if (_contador < 11)
                        ltbDetalle2.Dispatcher.BeginInvoke(
                            new System.Action(() => ltbDetalle2.Items.Add("Ping no exitoso! " + DateTime.Now)));
                    else
                    {
                        ltbDetalle2.Dispatcher.BeginInvoke(new System.Action(() =>
                        {
                            ltbDetalle2.Items.Add("Ping no exitoso! " + DateTime.Now);
                            ltbDetalle2.Items.RemoveAt(0);
                        }));
                    }
                }
                else if (_contador >= 11)
                {
                    ltbDetalle2.Dispatcher.BeginInvoke(new System.Action(() =>
                    {
                        ltbDetalle2.Items.Add("Respuesta desde " + data.Address +
                                              ": bytes=" + data.Buffer.Length +
                                              " tiempo=" + data.RoundtripTime + "ms" +
                                              " TTL=" + data.Options.Ttl +
                                              "  hora=" + DateTime.Now);
                        ltbDetalle2.Items.RemoveAt(0);
                    }));
                }
                else
                {
                    ltbDetalle2.Dispatcher.BeginInvoke(new System.Action(() =>
                    {
                        ltbDetalle2.Items.Add("Respuesta desde " + data.Address +
                                              ": bytes=" + data.Buffer.Length +
                                              " tiempo=" + data.RoundtripTime + "ms" +
                                              " TTL=" + data.Options.Ttl +
                                              "  hora=" + DateTime.Now);
                    }));
                }
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "WindowsDetallePing.xaml.cs(metodo Tick_2) " + ex.Message);
            }
        }
        private void doPingDetal3(string ip)
        {
            try
            {
                var Pings = new System.Net.NetworkInformation.Ping();
                var data = Pings.Send(ip, timeOut);

                if (data.Status != System.Net.NetworkInformation.IPStatus.Success)
                {
                    ltbDetalle3.Dispatcher.BeginInvoke(new System.Action(() => ltbDetalle3.Items.Add("Ping no exitoso!")));
                    _tiempoTres.Start();
                    return;
                }

                _conteo = 0;
                _conteo++;
                _tiempoTres.Start();
                ltbDetalle3.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle3.Items.Add("Respuesta desde " + data.Address +
                                          ": bytes=" + data.Buffer.Length +
                                          " tiempo=" + data.RoundtripTime + "ms" +
                                          " TTL=" + data.Options.Ttl +
                                          "  hora=" + DateTime.Now);
                }));
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "WindowsDetallePing.xaml.cs(metodo doPingDetal3) " + ex.Message);
            }
        }
        private void Tick_3(object source, ElapsedEventArgs e)
        {
            try
            {
                var Pings = new System.Net.NetworkInformation.Ping();
                var data = Pings.Send(DatoIp.ipAux3, timeOut);

                _conteo++;
                _contador = _conteo;
                _tiempoTres.Start();

                if (data.Status != System.Net.NetworkInformation.IPStatus.Success)
                {
                    if (_contador < 11)
                        ltbDetalle3.Dispatcher.BeginInvoke(
                            new System.Action(() => ltbDetalle3.Items.Add("Ping no exitoso! " + DateTime.Now)));
                    else
                    {
                        ltbDetalle3.Dispatcher.BeginInvoke(new System.Action(() =>
                        {
                            ltbDetalle3.Items.Add("Ping no exitoso! " + DateTime.Now);
                            ltbDetalle3.Items.RemoveAt(0);
                        }));
                    }
                }
                else if (_contador >= 11)
                {
                    ltbDetalle3.Dispatcher.BeginInvoke(new System.Action(() =>
                    {
                        ltbDetalle3.Items.Add("Respuesta desde " + data.Address +
                                              ": bytes=" + data.Buffer.Length +
                                              " tiempo=" + data.RoundtripTime + "ms" +
                                              " TTL=" + data.Options.Ttl +
                                              "  hora=" + DateTime.Now);
                        ltbDetalle3.Items.RemoveAt(0);
                    }));
                }
                else
                {
                    ltbDetalle3.Dispatcher.BeginInvoke(new System.Action(() =>
                    {
                        ltbDetalle3.Items.Add("Respuesta desde " + data.Address +
                                              ": bytes=" + data.Buffer.Length +
                                              " tiempo=" + data.RoundtripTime + "ms" +
                                              " TTL=" + data.Options.Ttl +
                                              "  hora=" + DateTime.Now);
                    }));
                }
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "WindowsDetallePing.xaml.cs(metodo Tick_3) " + ex.Message);
            }
        }
        private void doPingDetal4(string ip)
        {
            try
            {
                var Pings = new System.Net.NetworkInformation.Ping();
                var data = Pings.Send(ip, timeOut);

                if (data.Status != System.Net.NetworkInformation.IPStatus.Success)
                {
                    ltbDetalle4.Dispatcher.BeginInvoke(new System.Action(() => ltbDetalle4.Items.Add("Ping no exitoso!")));
                    _tiempoCuatro.Start();
                    return;
                }

                _conteo = 0;
                _conteo++;
                _tiempoCuatro.Start();
                ltbDetalle4.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle4.Items.Add("Respuesta desde " + data.Address +
                                          ": bytes=" + data.Buffer.Length +
                                          " tiempo=" + data.RoundtripTime + "ms" +
                                          " TTL=" + data.Options.Ttl +
                                          "  hora=" + DateTime.Now);
                }));
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "WindowsDetallePing.xaml.cs(metodo doPingDetal4) " + ex.Message);
            }
        }
        private void Tick_4(object source, ElapsedEventArgs e)
        {
            try
            {
                var Pings = new System.Net.NetworkInformation.Ping();
                var data = Pings.Send(DatoIp.ipAux4, timeOut);

                _conteo++;
                _contador = _conteo;
                _tiempoCuatro.Start();

                if (data.Status != System.Net.NetworkInformation.IPStatus.Success)
                {
                    if (_contador < 11)
                        ltbDetalle4.Dispatcher.BeginInvoke(
                            new System.Action(() => ltbDetalle4.Items.Add("Ping no exitoso! " + DateTime.Now)));
                    else
                    {
                        ltbDetalle4.Dispatcher.BeginInvoke(new System.Action(() =>
                        {
                            ltbDetalle4.Items.Add("Ping no exitoso! " + DateTime.Now);
                            ltbDetalle4.Items.RemoveAt(0);
                        }));
                    }
                }
                else if (_contador >= 11)
                {
                    ltbDetalle4.Dispatcher.BeginInvoke(new System.Action(() =>
                    {
                        ltbDetalle4.Items.Add("Respuesta desde " + data.Address +
                                              ": bytes=" + data.Buffer.Length +
                                              " tiempo=" + data.RoundtripTime + "ms" +
                                              " TTL=" + data.Options.Ttl +
                                              "  hora=" + DateTime.Now);
                        ltbDetalle4.Items.RemoveAt(0);
                    }));
                }
                else
                {
                    ltbDetalle4.Dispatcher.BeginInvoke(new System.Action(() =>
                    {
                        ltbDetalle4.Items.Add("Respuesta desde " + data.Address +
                                              ": bytes=" + data.Buffer.Length +
                                              " tiempo=" + data.RoundtripTime + "ms" +
                                              " TTL=" + data.Options.Ttl +
                                              "  hora=" + DateTime.Now);
                    }));
                }
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "WindowsDetallePing.xaml.cs(metodo Tick_4) " + ex.Message);
            }
        }
        private void doPingDetal5(string ip)
        {
            try
            {
                var Pings = new System.Net.NetworkInformation.Ping();
                var data = Pings.Send(ip, timeOut);

                if (data.Status != System.Net.NetworkInformation.IPStatus.Success)
                {
                    ltbDetalle5.Dispatcher.BeginInvoke(new System.Action(() => ltbDetalle5.Items.Add("Ping no exitoso!")));
                    _tiempoCinco.Start();
                    return;
                }

                _conteo = 0;
                _conteo++;
                _tiempoCinco.Start();
                ltbDetalle5.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle5.Items.Add("Respuesta desde " + data.Address +
                                          ": bytes=" + data.Buffer.Length +
                                          " tiempo=" + data.RoundtripTime + "ms" +
                                          " TTL=" + data.Options.Ttl +
                                          "  hora=" + DateTime.Now);
                }));
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "WindowsDetallePing.xaml.cs(metodo doPingDetal5) " + ex.Message);
            }
        }
        private void Tick_5(object source, ElapsedEventArgs e)
        {
            try
            {
                var Pings = new System.Net.NetworkInformation.Ping();
                var data = Pings.Send(DatoIp.ipAux5, timeOut);

                _conteo++;
                _contador = _conteo;
                _tiempoCinco.Start();

                if (data.Status != System.Net.NetworkInformation.IPStatus.Success)
                {
                    if (_contador < 11)
                        ltbDetalle5.Dispatcher.BeginInvoke(
                            new System.Action(() => ltbDetalle5.Items.Add("Ping no exitoso! " + DateTime.Now)));
                    else
                    {
                        ltbDetalle5.Dispatcher.BeginInvoke(new System.Action(() =>
                        {
                            ltbDetalle5.Items.Add("Ping no exitoso! " + DateTime.Now);
                            ltbDetalle5.Items.RemoveAt(0);
                        }));
                    }
                }
                else if (_contador >= 11)
                {
                    ltbDetalle5.Dispatcher.BeginInvoke(new System.Action(() =>
                    {
                        ltbDetalle5.Items.Add("Respuesta desde " + data.Address +
                                              ": bytes=" + data.Buffer.Length +
                                              " tiempo=" + data.RoundtripTime + "ms" +
                                              " TTL=" + data.Options.Ttl +
                                              "  hora=" + DateTime.Now);
                        ltbDetalle5.Items.RemoveAt(0);
                    }));
                }
                else
                {
                    ltbDetalle5.Dispatcher.BeginInvoke(new System.Action(() =>
                    {
                        ltbDetalle5.Items.Add("Respuesta desde " + data.Address +
                                              ": bytes=" + data.Buffer.Length +
                                              " tiempo=" + data.RoundtripTime + "ms" +
                                              " TTL=" + data.Options.Ttl +
                                              "  hora=" + DateTime.Now);
                    }));
                }
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "WindowsDetallePing.xaml.cs(metodo Tick_5) " + ex.Message);
            }
        }
        private void doPingDetal6(string ip)
        {
            try
            {
                var Pings = new System.Net.NetworkInformation.Ping();
                var data = Pings.Send(ip, timeOut);

                if (data.Status != System.Net.NetworkInformation.IPStatus.Success)
                {
                    ltbDetalle6.Dispatcher.BeginInvoke(new System.Action(() => ltbDetalle6.Items.Add("Ping no exitoso!")));
                    _tiempoSeis.Start();
                    return;
                }

                _conteo = 0;
                _conteo++;
                _tiempoSeis.Start();
                ltbDetalle6.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle6.Items.Add("Respuesta desde " + data.Address +
                                          ": bytes=" + data.Buffer.Length +
                                          " tiempo=" + data.RoundtripTime + "ms" +
                                          " TTL=" + data.Options.Ttl +
                                          "  hora=" + DateTime.Now);
                }));
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "WindowsDetallePing.xaml.cs(metodo doPingDetal6) " + ex.Message);
            }
        }
        private void Tick_6(object source, ElapsedEventArgs e)
        {
            try
            {
                var Pings = new System.Net.NetworkInformation.Ping();
                var data = Pings.Send(DatoIp.ipAux6, timeOut);

                _conteo++;
                _contador = _conteo;
                _tiempoSeis.Start();

                if (data.Status != System.Net.NetworkInformation.IPStatus.Success)
                {
                    if (_contador < 11)
                        ltbDetalle6.Dispatcher.BeginInvoke(
                            new System.Action(() => ltbDetalle6.Items.Add("Ping no exitoso! " + DateTime.Now)));
                    else
                    {
                        ltbDetalle6.Dispatcher.BeginInvoke(new System.Action(() =>
                        {
                            ltbDetalle6.Items.Add("Ping no exitoso! " + DateTime.Now);
                            ltbDetalle6.Items.RemoveAt(0);
                        }));
                    }
                }
                else if (_contador >= 11)
                {
                    ltbDetalle6.Dispatcher.BeginInvoke(new System.Action(() =>
                    {
                        ltbDetalle6.Items.Add("Respuesta desde " + data.Address +
                                              ": bytes=" + data.Buffer.Length +
                                              " tiempo=" + data.RoundtripTime + "ms" +
                                              " TTL=" + data.Options.Ttl +
                                              "  hora=" + DateTime.Now);
                        ltbDetalle6.Items.RemoveAt(0);
                    }));
                }
                else
                {
                    ltbDetalle6.Dispatcher.BeginInvoke(new System.Action(() =>
                    {
                        ltbDetalle6.Items.Add("Respuesta desde " + data.Address +
                                              ": bytes=" + data.Buffer.Length +
                                              " tiempo=" + data.RoundtripTime + "ms" +
                                              " TTL=" + data.Options.Ttl +
                                              "  hora=" + DateTime.Now);
                    }));
                }
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "WindowsDetallePing.xaml.cs(metodo Tick_6) " + ex.Message);
            }
        }
        private void ShowListBox1()
        {
            try
            {
                ltbDetalle1.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle1.Visibility = Visibility.Visible;
                }));
                ltbDetalle2.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle2.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle3.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle3.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle4.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle4.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle5.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle5.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle6.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle6.Visibility = Visibility.Collapsed;
                }));
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "WindowsDetallePing.xaml.cs(metodo ShowListBox1) " + ex.Message);
            }
        }
        private void ShowListBox2()
        {
            try
            {
                ltbDetalle1.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle1.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle2.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle2.Visibility = Visibility.Visible;
                }));
                ltbDetalle3.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle3.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle4.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle4.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle5.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle5.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle6.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle6.Visibility = Visibility.Collapsed;
                }));
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "WindowsDetallePing.xaml.cs(metodo ShowListBox2) " + ex.Message);
            }
        }
        private void ShowListBox3()
        {
            try
            {
                ltbDetalle1.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle1.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle2.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle2.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle3.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle3.Visibility = Visibility.Visible;
                }));
                ltbDetalle4.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle4.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle5.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle5.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle6.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle6.Visibility = Visibility.Collapsed;
                }));
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "WindowsDetallePing.xaml.cs(metodo ShowListBox3) " + ex.Message);
            }
        }
        private void ShowListBox4()
        {
            try
            {
                ltbDetalle1.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle1.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle2.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle2.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle3.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle3.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle4.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle4.Visibility = Visibility.Visible;
                }));
                ltbDetalle5.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle5.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle6.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle6.Visibility = Visibility.Collapsed;
                }));
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "WindowsDetallePing.xaml.cs(metodo ShowListBox4) " + ex.Message);
            }
        }
        private void ShowListBox5()
        {
            try
            {
                ltbDetalle1.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle1.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle2.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle2.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle3.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle3.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle4.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle4.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle5.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle5.Visibility = Visibility.Visible;
                }));
                ltbDetalle6.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle6.Visibility = Visibility.Collapsed;
                }));
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "WindowsDetallePing.xaml.cs(metodo ShowListBox5) " + ex.Message);
            }
        }
        private void ShowListBox6()
        {
            try
            {
                ltbDetalle1.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle1.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle2.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle2.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle3.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle3.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle4.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle4.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle5.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle5.Visibility = Visibility.Collapsed;
                }));
                ltbDetalle6.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    ltbDetalle6.Visibility = Visibility.Visible;
                }));
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "WindowsDetallePing.xaml.cs(metodo ShowListBox6) " + ex.Message);
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatoIp.nroEntradas--;
                if (DatoIp.nroEntradas < 0)
                    DatoIp.nroEntradas = 0;
                this.Close();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "WindowsDetallePing.xaml.cs(metodo btnClose_Click) " + ex.Message);
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                DatoIp.nroEntradas--;
                if (DatoIp.nroEntradas < 0)
                    DatoIp.nroEntradas = 0;
                this.Close();
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, System.DateTime.Now, Environment.UserName, "WindowsDetallePing.xaml.cs(metodo Window_Closed) " + ex.Message);
            }
        }

    }
}
