using System.Collections.Generic;
using Ping.Accion;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Network
{
    public static class Ping
    {
        public static IPAddress ResolveAddress(string hostNameOrAddress)
        {
            IPAddress address;
            if (!IPAddress.TryParse(hostNameOrAddress, out address))
            {
                //resuelve el nombre de la url y retorna la ip
                var addresses = Dns.GetHostAddresses(hostNameOrAddress);
                address = addresses[0];
            }
            return address;
        }

        public static async Task<IPAddress> ResolveAddressAsync(string hostNameOrAddress)
        {
            IPAddress address;
            if (!IPAddress.TryParse(hostNameOrAddress, out address))
            {
                //resuelve el nombre de la url y retorna la ip
                var addresses = await Dns.GetHostAddressesAsync(hostNameOrAddress);
                address = addresses[0];
            }
            return address;
        }

        public static async Task ContinuousPingAsync(string address, int timeout, int timeEntrePing,byte[] tamanoPaquete, IProgress<PingResult> progress, CancellationToken cancellationToken, List<string> ips)
        {
            var ping = new System.Net.NetworkInformation.Ping();
            var result = new PingResult(IPAddress.Parse(address), timeEntrePing);

            while (!cancellationToken.IsCancellationRequested)
            {
                var res = await ping.SendPingAsync(address, timeout, tamanoPaquete).ConfigureAwait(false);
                result.AddResult(res);
                progress.Report(result);
                if (ips != null && ips.Contains(address))
                    await Task.Delay(FrecuenciaNoPing()).ConfigureAwait(false);
                else
                    await Task.Delay(timeEntrePing).ConfigureAwait(false);
            }
        }
        private static int FrecuenciaNoPing()
        {
            try
            {
                var configuracionGeneralAction = new ConfiguracionGeneral_action();
                var timeEntrePig = configuracionGeneralAction.GetConfigGeneral().Frecuencia_no_ping;
                return (int)timeEntrePig;
            }
            catch (Exception ex)
            {
                var logeer = new LogErroresModificaciones__action();
                logeer.InsertErroresLog(1, DateTime.Now, Environment.UserName, "PingTaskViewModel.cs(metodo FrecuenciaNoPing) " + ex.Message);
                return 1;
            }
        }
    }
}
