using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using parking_control.Models;

namespace parking_control.Service.Model
{
    public class VehicleEntranceModel
    {
        public static void Insert(VehicleEntrance vehicle)
        {
            string initialDate = vehicle.DateIn.ToString("yyyy-MM-dd HH:mm:ss");
            string finalDate = vehicle.DateOut.ToString("yyyy-MM-dd HH:mm:ss");
            string sql = string.Format(
                    "INSERT INTO VehicleEntrance (HourPrice, Board, DateIn, DateOut, PriceCharged) VALUES ({0}, \"{1}\", \"{2}\", \"{3}\", {4});"
                    , MySqlHelper.DoubleQuoteString(vehicle.HourPrice.ToString()).Replace(",", "."), vehicle.Board, initialDate, finalDate, MySqlHelper.DoubleQuoteString(vehicle.PriceCharged.ToString()).Replace(",", "."));

            using (MySqlCommand command = new MySqlCommand(sql, ConnectMysql.GetInstance()))
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

        public static VehicleEntrance Select(string vehicleBoard)
        {
            VehicleEntrance vehicle = null;
            string sql = string.Format("SELECT * FROM VehicleEntrance WHERE Board = \"{0}\"", vehicleBoard);
            using (MySqlCommand command = new MySqlCommand(sql, ConnectMysql.GetInstance()))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                        throw new NotExecuteCommandSql("Erro na leitura de uma data do banco ou a placa do veiculo não existe na base");

                    int id = reader.GetInt32(0);
                    double hourPrice = reader.GetDouble(1);                    
                    DateTime dateTimeInitial = reader.GetDateTime(3);
                    DateTime dateTimeFinal = reader.GetDateTime(4);
                    double priceCharged = reader.GetDouble(5);
                    vehicle = new VehicleEntrance(id, vehicleBoard, dateTimeInitial);
                    vehicle.DateOut = dateTimeFinal;
                    vehicle.HourPrice = hourPrice;
                    vehicle.PriceCharged = priceCharged;
                }
            }
            return vehicle;
        }

        public static VehicleEntrance Select(int id)
        {
            if (id == 0)
                throw new NotFoundIDEntity("A chave primaria não pode ser igual a 0");
            
            VehicleEntrance vehicle = null;
            string sql = string.Format("SELECT * FROM VehicleEntrance WHERE id = {0}", id);
            using (MySqlCommand command = new MySqlCommand(sql, ConnectMysql.GetInstance()))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                        throw new NotExecuteCommandSql("Erro na leitura de uma data do banco ou a placa do veiculo não existe na base");
                    
                    double hourPrice = reader.GetDouble(1);
                    string board = reader.GetString(2);
                    DateTime dateTimeInitial = reader.GetDateTime(3);
                    DateTime dateTimeFinal = reader.GetDateTime(4);
                    double priceCharged = reader.GetDouble(5);
                    vehicle = new VehicleEntrance(id, board, dateTimeInitial);
                    vehicle.DateOut = dateTimeFinal;
                    vehicle.HourPrice = hourPrice;
                    vehicle.PriceCharged = priceCharged;
                }
            }
            return vehicle;
        }

        public static Dictionary<string, VehicleEntrance> SelectAll(int limit = 1000)
        {
            Dictionary<string, VehicleEntrance> dict = new Dictionary<string, VehicleEntrance>();
            string sql = string.Format("SELECT * FROM VehicleEntrance LIMIT {0}", limit);
            using (MySqlCommand command = new MySqlCommand(sql, ConnectMysql.GetInstance()))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        double hourPrice = reader.GetDouble(1);
                        string board = reader.GetString(2);
                        DateTime dateIn = reader.GetDateTime(3);
                        DateTime dateOut = reader.GetDateTime(4);
                        dict[board] = new VehicleEntrance(id, hourPrice, board, dateIn, dateOut);                        
                    }
                }
            }
            return dict;
        }

        public static void Update(VehicleEntrance vehicle)
        {
            if (vehicle.ID == 0)
                throw new NotFoundIDEntity("A objeto atualizado não possue uma chave primária");
            string initialDate = vehicle.DateIn.ToString("yyyy-MM-dd HH:mm:ss");
            string finalDate = vehicle.DateOut.ToString("yyyy-MM-dd HH:mm:ss");
            string sql = string.Format(
                    "UPDATE VehicleEntrance SET HourPrice={0}, Board=\"{1}\", DateIn=\"{2}\", DateOut=\"{3}\", PriceCharged={4} WHERE id = {5};"
                    , MySqlHelper.DoubleQuoteString(vehicle.HourPrice.ToString()).Replace(",", "."), vehicle.Board, initialDate, finalDate, MySqlHelper.DoubleQuoteString(vehicle.PriceCharged.ToString()).Replace(",", "."), vehicle.ID);

            using (MySqlCommand command = new MySqlCommand(sql, ConnectMysql.GetInstance()))
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }

        }

        public static void Delete(VehicleEntrance vehicle)
        {
            DeleteByID(vehicle.ID);
        }

        public static void DeleteByID(int id)
        {
            if (id == 0)
                throw new NotFoundIDEntity("A objeto excluido não possue uma chave primária");
            string sql = string.Format("DELETE FROM VehicleEntrance WHERE id = {0}", id);
            using (MySqlCommand command = new MySqlCommand(sql, ConnectMysql.GetInstance()))
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }

        }
    }
}