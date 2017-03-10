using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using parking_control.Models;

namespace parking_control.Service.Model
{
    public class ValidityDateControlModel
    {        
        public static ValidityDateControl Select(DateTime InitialDate)
        {
            ValidityDateControl dateControl = null;
            string initialDateString = InitialDate.ToString("yyyy-MM-dd HH:mm:ss");
            string sql = string.Format(
                "SELECT * FROM ValidityDateControl WHERE InitialDate = \"{0}\""
                , initialDateString);
            using (MySqlCommand command = new MySqlCommand(sql, ConnectMysql.GetInstance()))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                        throw new NotExecuteCommandSql("Erro na leitura de uma data do banco");

                    int id = reader.GetInt32(0);
                    double hourPrice = reader.GetDouble(1);
                    DateTime dateTimeInitial = reader.GetDateTime(2);
                    DateTime dateTimeFinal = reader.GetDateTime(3);
                    dateControl = new ValidityDateControl(id, hourPrice, dateTimeInitial, dateTimeFinal);
                }
            }
            return dateControl;
        }

        public static ValidityDateControl Select(int id)
        {
            if (id == 0)
                throw new NotFoundIDEntity("A chave primaria não pode ser igual a 0");

            ValidityDateControl dateControl = null;
            string sql = string.Format(
                "SELECT * FROM ValidityDateControl WHERE id = \"{0}\""
                , id);
            using (MySqlCommand command = new MySqlCommand(sql, ConnectMysql.GetInstance()))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                        throw new NotExecuteCommandSql("Erro na leitura de uma data do banco ou a chave primária não existe na base");

                    double hourPrice = reader.GetDouble(1);
                    DateTime dateTimeInitial = reader.GetDateTime(2);
                    DateTime dateTimeFinal = reader.GetDateTime(3);
                    dateControl = new ValidityDateControl(id, hourPrice, dateTimeInitial, dateTimeFinal);
                }
            }
            return dateControl;
        }

        public static List<ValidityDateControl> SelectAll(int limit = 1000)
        {
            List<ValidityDateControl> listDateControl = new List<ValidityDateControl>();            
            string sql = string.Format("SELECT * FROM ValidityDateControl LIMIT {0}", limit);
            using (MySqlCommand command = new MySqlCommand(sql, ConnectMysql.GetInstance()))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                        throw new NotExecuteCommandSql("Erro na leitura de uma data do banco ou a chave primária não existe na base");
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        double hourPrice = reader.GetDouble(1);
                        DateTime dateTimeInitial = reader.GetDateTime(2);
                        DateTime dateTimeFinal = reader.GetDateTime(3);
                        listDateControl.Add(new ValidityDateControl(id, hourPrice, dateTimeInitial, dateTimeFinal));
                    }

                }
            }
            return listDateControl;
        }

        public static void Insert(ValidityDateControl dateControl)
        {
            string initialDate = dateControl.InitialDate.ToString("yyyy-MM-dd HH:mm:ss");
            string finalDate = dateControl.FinalDate.ToString("yyyy-MM-dd HH:mm:ss");
            string sql = string.Format(
                    "INSERT INTO ValidityDateControl (HourPrice, InitialDate, FinalDate) VALUES ({0}, \"{1}\", \"{2}\");"
                    , dateControl.HourPrice, initialDate, finalDate);

            using (MySqlCommand command = new MySqlCommand(sql, ConnectMysql.GetInstance()))
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

        public static void Update(ValidityDateControl dateControl)
        {
            if (dateControl.ID == 0)
                throw new NotFoundIDEntity("A objeto atualizado não possue uma chave primária");
            string initialDate = dateControl.InitialDate.ToString("yyyy-MM-dd HH:mm:ss");
            string finalDate = dateControl.FinalDate.ToString("yyyy-MM-dd HH:mm:ss");
            string sql = string.Format(
                    "UPDATE ValidityDateControl SET HourPrice={0}, InitialDate=\"{1}\", FinalDate=\"{2}\" WHERE id = {3};"
                    , dateControl.HourPrice, initialDate, finalDate, dateControl.ID);

            using (MySqlCommand command = new MySqlCommand(sql, ConnectMysql.GetInstance()))
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteAll()
        {
            string sql = string.Format("DELETE FROM ValidityDateControl");
            using (MySqlCommand command = new MySqlCommand(sql, ConnectMysql.GetInstance()))
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

        public static void Delete(ValidityDateControl dateControl)
        {
            if (dateControl.ID == 0)
                throw new NotFoundIDEntity("A objeto excluido não possue uma chave primária");
            string sql = string.Format("DELETE FROM ValidityDateControl WHERE id = {0}", dateControl.ID);
            using (MySqlCommand command = new MySqlCommand(sql, ConnectMysql.GetInstance()))
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

    }

    
}