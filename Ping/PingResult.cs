using Ping.Accion;
using Ping.DAO;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Network
{
    public class PingResult
    {
        public IPAddress Address { get; private set; }
        public string Res { get; private set; }
        public int PingsTotal { get; private set; }
        public int PingsSuccessfull { get; private set; }
        public TimeSpan AverageTime { get; private set; }
        public TimeSpan LastTime { get; private set; }
        public IPStatus LastStatus { get; private set; }
        public int TimeEntrePing { get; set; }
        public PingResult(IPAddress address, int timeEntrePing)
        {
            Address = address;
            LastStatus = IPStatus.Unknown;
            TimeEntrePing = timeEntrePing;
        }
        public void AddResult(PingReply res)
        {
            PingsTotal++;
            LastStatus = res.Status;
            
            if (res.Status == IPStatus.Success)
            {
                PingsSuccessfull++;
                LastTime = TimeSpan.FromMilliseconds(res.RoundtripTime);
                if (PingsSuccessfull == 1)
                    AverageTime = LastTime;
                else
                {
                    var oldAverage = AverageTime.TotalMilliseconds;
                    AverageTime = TimeSpan.FromMilliseconds(oldAverage + (res.RoundtripTime - oldAverage) / PingsSuccessfull);
                }
                Monitoreo_DAO.InsertMonitoreo(Address.ToString(), DateTime.Now, true, res.RoundtripTime, TimeEntrePing);
            }
            else
            {
                LastTime = TimeSpan.Zero;
                Monitoreo_DAO.InsertMonitoreo(Address.ToString(), DateTime.Now, false, res.RoundtripTime, TimeEntrePing);
            }
            //Medicion de jitter: la diferencia entre latencias, y se divide por la cantidad de latencias, menos 1.
           }

        //public static DataTable creDatatable()
        //{
        //    DataTable _dt = new DataTable();
        //    _dt.Columns.Add("IP_EQUIPO", typeof(string));
        //    _dt.Columns.Add("TIMESTAMP", typeof(DateTime));
        //    _dt.Columns.Add("EXITO_PING", typeof(bool));
        //    _dt.Columns.Add("LATENCIA", typeof(double));
        //    _dt.Columns.Add("JITTER", typeof(double));
        //    _dt.Columns.Add("TIEMPO_ENTRE_PING", typeof(double));
        //    return _dt;
        //}
        //public static async Task InsertaRegistros(string ip, DateTime timespamp, bool exito, double latencia, double jitter, double timeEntrePing)
        //{
        //    try
        //    {

        //        //if (_dt == null || _dt.Columns.Count <= 0)
        //        //{
        //        //    _dt.Columns.Add("IP_EQUIPO", typeof(string));
        //        //    _dt.Columns.Add("TIMESTAMP", typeof(DateTime));
        //        //    _dt.Columns.Add("EXITO_PING", typeof(bool));
        //        //    _dt.Columns.Add("LATENCIA", typeof(double));
        //        //    _dt.Columns.Add("JITTER", typeof(double));
        //        //    _dt.Columns.Add("TIEMPO_ENTRE_PING", typeof(double));
        //        //}
        //        _dt.Rows.Add(ip, timespamp, exito, latencia, jitter, timeEntrePing);

        //        if (_dt.Rows.Count < 500) return;

        //        var kkck = 99;
                
        //        using (var cnx = new SqlConnection(_conexion))
        //        {
        //            cnx.Open();
        //            using (var copy = new SqlBulkCopy(cnx))
        //            {
        //                copy.DestinationTableName = "MONITOREO";
        //                copy.WriteToServer(_dt);
        //                _dt.Clear();
        //                cnx.Close();
        //                cnx.Dispose();
        //            }
        //        }
        //        _dt = null;//
        //    }
        //    catch (Exception ex)
        //    {
        //        var a = ex.Message;
        //    }
        //}

        //public void AddResult(PingReply res)
        //{
        //    PingsTotal++;
        //    LastStatus = res.Status;


        //    if (res.Status == IPStatus.Success)
        //    {
        //        PingsSuccessfull++;
        //        LastTime = TimeSpan.FromMilliseconds(res.RoundtripTime);
        //        if (PingsSuccessfull == 1)
        //            AverageTime = LastTime;
        //        else
        //        {
        //            var oldAverage = AverageTime.TotalMilliseconds;
        //            AverageTime = TimeSpan.FromMilliseconds(oldAverage + (res.RoundtripTime - oldAverage) / PingsSuccessfull);
        //        }
        //        //var timeEntrePing = TimeEntrePing;
        //        //var latenciaAnterior = monitoreo_action.GetLatenciaAnterior(Address.ToString());
        //        //var jitter = Math.Abs(((latenciaAnterior - res.RoundtripTime) / 2));

        //        //InsertaRegistros(Address.ToString(), DateTime.Now, true, res.RoundtripTime, 1, TimeEntrePing);
        //        //Monitoreo_DAO.InsertMonitoreo(Address.ToString(), DateTime.Now, true, res.RoundtripTime, 1, TimeEntrePing);
        //        //try
        //        //{

        //        //    //exto =texto +(Address.ToString() + " , " + DateTime.Now.ToShortDateString()+" "+ DateTime.Now.ToLongTimeString() +" "+DateTime.Now.Millisecond+ " , " + true + "," + res.RoundtripTime + " , " + 1 + " , " + TimeEntrePing).ToString()+"*";

        //        //    //System.IO.StreamWriter sw = new System.IO.StreamWriter(fic,true);
        //        //    //sw.WriteLine(texto);
        //        //    //sw.Flush();
        //        //    //sw.Close();
        //        //}
        //        //catch (Exception ex)
        //        //{
        //        //    var a = ex.Message;
        //        //}

        //    }
        //    else
        //    {
        //        LastTime = TimeSpan.Zero;
        //        //var timeEntrePing = TimeEntrePing;
        //        //var latenciaAnterior = monitoreo_action.GetLatenciaAnterior(Address.ToString());
        //        //var jitter = Math.Abs(((latenciaAnterior - res.RoundtripTime) / 2));

        //        //InsertaRegistros(Address.ToString(), DateTime.Now, false, res.RoundtripTime, 1, TimeEntrePing);
        //        //Monitoreo_DAO.InsertMonitoreo(Address.ToString(), DateTime.Now, false, res.RoundtripTime, 2, TimeEntrePing);
        //        //try
        //        //{
        //        //    texto = texto + (Address.ToString() + " , " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " " + DateTime.Now.Millisecond + " , " + false + " , " + res.RoundtripTime + " , " + 1 + "," + TimeEntrePing).ToString() + "*";
        //        //    //System.IO.StreamWriter sw = new System.IO.StreamWriter(fic,true);
        //        //    //sw.WriteLine(texto);
        //        //    //sw.Flush();
        //        //    //sw.Close();
        //        //}
        //        //catch (Exception ex)
        //        //{
        //        //    var a = ex.Message;
        //        //}
        //    }
        //    //Medicion de jitter: la diferencia entre latencias, y se divide por la cantidad de latencias, menos 1.

        //}

    }
}
