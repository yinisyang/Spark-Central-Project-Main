using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SparkAPI.Models;

namespace SparkAPI
{
    public class MemberPersistence
    {
        private SqlConnection conn;

        public MemberPersistence()
        {
            try
            {
                conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/SparkAPI").ConnectionStrings.ConnectionStrings["SparkCentralConnectionString"].ConnectionString);
                conn.Open();
            }
            catch (SqlException ex)
            {

            }
        }

        public ArrayList getMembers(string ethnicity, bool? restricted_to_tech, bool? west_central_resident, string email)
        {
            String sqlString = "SELECT * FROM Members ";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            if(ethnicity != null || restricted_to_tech != null || west_central_resident != null || email != null)
            {
                sqlString += "WHERE ";
                if(ethnicity != null)
                {
                    sqlString += "ethnicity = @ethnicity AND ";
                    SqlParameter ethnicityParam = new SqlParameter("@ethnicity", System.Data.SqlDbType.VarChar, 50);
                    ethnicityParam.Value = ethnicity;
                    cmd.Parameters.Add(ethnicityParam);
                }
                if(restricted_to_tech != null)
                {
                    sqlString += "restricted_to_tech = @restricted_to_tech AND ";
                    SqlParameter techParam = new SqlParameter("@restricted_to_tech", System.Data.SqlDbType.Bit);
                    techParam.Value = restricted_to_tech;
                    cmd.Parameters.Add(techParam);
                }
                if(west_central_resident != null)
                {
                    sqlString += "west_central_resident = @west_central_resident AND ";
                    SqlParameter westCentralParam = new SqlParameter("@west_central_resident", System.Data.SqlDbType.Bit);
                    westCentralParam.Value = west_central_resident;
                    cmd.Parameters.Add(westCentralParam);
                }
                if(email != null)
                {
                    sqlString += "email = @email";
                    SqlParameter emailParam = new SqlParameter("@email", System.Data.SqlDbType.VarChar, 50);
                    emailParam.Value = email;
                    cmd.Parameters.Add(emailParam);
                }
            }
            if (sqlString.Substring(sqlString.Length - 4).Equals("AND "))
                sqlString = sqlString.Substring(0, sqlString.Length - 4);

            sqlString += ";";

            cmd.CommandText = sqlString;
            cmd.Prepare();
            SqlDataReader reader = cmd.ExecuteReader();

            ArrayList MemberArray = new ArrayList();

            while (reader.Read())
            {
                Member member = new Member();

                member.member_id = reader.GetInt32(reader.GetOrdinal("member_id"));
                member.first_name = reader.GetString(reader.GetOrdinal("first_name"));
                member.last_name = reader.GetString(reader.GetOrdinal("last_name"));
                //guardian_name is a nullable data field
                if (reader["guardian_name"] != DBNull.Value)
                {
                    member.guardian_name = reader.GetString(reader.GetOrdinal("guardian_name"));
                }
                else
                {
                    member.guardian_name = null;
                }
                member.email = reader.GetString(reader.GetOrdinal("email"));
                member.dob = reader.GetDateTime(reader.GetOrdinal("dob"));
                member.phone = reader.GetString(reader.GetOrdinal("phone"));
                member.street_address = reader.GetString(reader.GetOrdinal("street_address"));
                member.city = reader.GetString(reader.GetOrdinal("city"));
                member.state = reader.GetString(reader.GetOrdinal("state"));
                member.zip = reader.GetInt32(reader.GetOrdinal("zip"));
                member.quota = reader.GetInt32(reader.GetOrdinal("quota"));
                member.is_adult = reader.GetBoolean(reader.GetOrdinal("is_adult"));
                member.ethnicity = reader.GetString(reader.GetOrdinal("ethnicity"));
                member.restricted_to_tech = reader.GetBoolean(reader.GetOrdinal("restricted_to_tech"));
                member.west_central_resident = reader.GetBoolean(reader.GetOrdinal("west_central_resident"));

                MemberArray.Add(member);

            }
            conn.Close();
            return MemberArray;
        }

        public Member getMember(int id)
        {
            String sqlString = "SELECT * FROM Members WHERE member_id = @id;";
            SqlCommand cmd = new SqlCommand(sqlString, conn);

            //sql parameters for protection
            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int, 4);
            idParam.Value = id;

            cmd.Parameters.Add(idParam);
            cmd.Prepare();
            ///////

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Member toReturn = new Member();

                toReturn.member_id = reader.GetInt32(reader.GetOrdinal("member_id"));
                toReturn.first_name = reader.GetString(reader.GetOrdinal("first_name"));
                toReturn.last_name = reader.GetString(reader.GetOrdinal("last_name"));
                //guardian_name is a nullable data field
                if (reader["guardian_name"] != DBNull.Value)
                {
                    toReturn.guardian_name = reader.GetString(reader.GetOrdinal("guardian_name"));
                }
                else
                {
                    toReturn.guardian_name = null;
                }
                toReturn.email = reader.GetString(reader.GetOrdinal("email"));
                toReturn.dob = reader.GetDateTime(reader.GetOrdinal("dob"));
                toReturn.phone = reader.GetString(reader.GetOrdinal("phone"));
                toReturn.street_address = reader.GetString(reader.GetOrdinal("street_address"));
                toReturn.city = reader.GetString(reader.GetOrdinal("city"));
                toReturn.state = reader.GetString(reader.GetOrdinal("state"));
                toReturn.zip = reader.GetInt32(reader.GetOrdinal("zip"));
                toReturn.quota = reader.GetInt32(reader.GetOrdinal("quota"));
                toReturn.is_adult = reader.GetBoolean(reader.GetOrdinal("is_adult"));
                toReturn.ethnicity = reader.GetString(reader.GetOrdinal("ethnicity"));
                toReturn.restricted_to_tech = reader.GetBoolean(reader.GetOrdinal("restricted_to_tech"));
                toReturn.west_central_resident = reader.GetBoolean(reader.GetOrdinal("west_central_resident"));

                conn.Close();
                return toReturn;
            }
            conn.Close();
            return null;
        }

        public int saveMember(Member memberToSave)
        {
            String sqlString = "INSERT INTO Members (first_name, last_name, guardian_name, email, dob, phone, " +
                "street_address, city, state, zip, quota, is_adult, ethnicity, restricted_to_tech, west_central_resident) " +
                "OUTPUT INSERTED.member_id VALUES (@first_name, @last_name, @guardian_name, @email, @dob, @phone, " +
                "@street_address, @city, @state, @zip, @quota, @is_adult, @ethnicity, @restricted_to_tech, @west_central_resident)";

            SqlParameter first_nameParam = new SqlParameter("@first_name", System.Data.SqlDbType.VarChar, 50);
            SqlParameter last_nameParam = new SqlParameter("@last_name", System.Data.SqlDbType.VarChar, 50);
            SqlParameter guardian_nameParam = new SqlParameter("@guardian_name", System.Data.SqlDbType.VarChar, 50);
            SqlParameter emailParam = new SqlParameter("@email", System.Data.SqlDbType.VarChar, 50);
            SqlParameter dobParam = new SqlParameter("@dob", System.Data.SqlDbType.Date, 3);
            SqlParameter phoneParam = new SqlParameter("@phone", System.Data.SqlDbType.VarChar, 50);
            SqlParameter street_addressParam = new SqlParameter("@street_address", System.Data.SqlDbType.VarChar, 50);
            SqlParameter cityParam = new SqlParameter("@city", System.Data.SqlDbType.VarChar, 50);
            SqlParameter stateParam = new SqlParameter("@state", System.Data.SqlDbType.VarChar, 50);
            SqlParameter zipParam = new SqlParameter("@zip", System.Data.SqlDbType.Int, 4);
            SqlParameter quotaParam = new SqlParameter("@quota", System.Data.SqlDbType.Int, 4);
            SqlParameter isAdultParam = new SqlParameter("@is_adult", System.Data.SqlDbType.Bit, 1);
            SqlParameter ethnicityParam = new SqlParameter("@ethnicity", System.Data.SqlDbType.VarChar, 50);
            SqlParameter restricted_to_techParam = new SqlParameter("@restricted_to_tech", System.Data.SqlDbType.Bit, 1);
            SqlParameter west_central_residentParam = new SqlParameter("@west_central_resident", System.Data.SqlDbType.Bit, 1);

            first_nameParam.Value = memberToSave.first_name;
            last_nameParam.Value = memberToSave.last_name;
            guardian_nameParam.Value = (object)memberToSave.guardian_name ?? DBNull.Value;
            emailParam.Value = memberToSave.email;
            dobParam.Value = memberToSave.dob;
            phoneParam.Value = memberToSave.phone;
            street_addressParam.Value = memberToSave.street_address;
            cityParam.Value = memberToSave.city;
            stateParam.Value = memberToSave.state;
            zipParam.Value = memberToSave.zip;
            quotaParam.Value = memberToSave.quota;
            isAdultParam.Value = memberToSave.is_adult;
            ethnicityParam.Value = memberToSave.ethnicity;
            restricted_to_techParam.Value = memberToSave.restricted_to_tech;
            west_central_residentParam.Value = memberToSave.west_central_resident;

            SqlCommand cmd = new SqlCommand(sqlString, conn);

            cmd.Parameters.Add(first_nameParam);
            cmd.Parameters.Add(last_nameParam);
            cmd.Parameters.Add(guardian_nameParam);
            cmd.Parameters.Add(emailParam);
            cmd.Parameters.Add(dobParam);
            cmd.Parameters.Add(phoneParam);
            cmd.Parameters.Add(street_addressParam);
            cmd.Parameters.Add(cityParam);
            cmd.Parameters.Add(stateParam);
            cmd.Parameters.Add(zipParam);
            cmd.Parameters.Add(quotaParam);
            cmd.Parameters.Add(isAdultParam);
            cmd.Parameters.Add(ethnicityParam);
            cmd.Parameters.Add(restricted_to_techParam);
            cmd.Parameters.Add(west_central_residentParam);

            cmd.Prepare();

            int id = (int)cmd.ExecuteScalar();
            conn.Close();
            return id;
        }
        public bool deleteMember(int id)
        {
            String sqlString = "SELECT * FROM Members WHERE member_id = @id;";
            SqlCommand cmd = new SqlCommand(sqlString, conn);

            //sql parameters for protection
            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int, 4);
            idParam.Value = id;

            cmd.Parameters.Add(idParam);
            cmd.Prepare();
            ///////

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                reader.Close();

                //Iutilized new variables rather than reusing the previous ones due to errors caused if I don't
                String sqlString2 = "DELETE FROM Members WHERE member_id = @id2;";
                SqlCommand delcmd = new SqlCommand(sqlString2, conn);

                SqlParameter idParam2 = new SqlParameter("@id2", System.Data.SqlDbType.Int, 4);
                idParam2.Value = idParam.Value;
                delcmd.Parameters.Add(idParam2);

                delcmd.Prepare();

                delcmd.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool updateMember(int member_id, Member toUpdate)
        {
            String sqlString = "SELECT * FROM Members WHERE member_id = @member_id;";
            SqlCommand cmd = new SqlCommand(sqlString, conn);

            //sql parameters for protection
            SqlParameter idParam = new SqlParameter("@member_id", System.Data.SqlDbType.Int, 4);
            idParam.Value = member_id;

            cmd.Parameters.Add(idParam);
            cmd.Prepare();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                reader.Close();

                String sqlString3 = "UPDATE Members SET first_name=@first_name, last_name=@last_name, guardian_name=@guardian_name, " +
                    " email=@email, dob=@dob, phone=@phone, street_address=@street_address, city=@city, state=@state, " +
                    "zip=@zip, quota=@quota, is_adult=@is_adult, ethnicity=@ethnicity, restricted_to_tech=@restricted_to_tech, " +
                    "west_central_resident=@west_central_resident WHERE member_id=@membeId";
                SqlCommand upcmd = new SqlCommand(sqlString3, conn);

                SqlParameter first_nameParam = new SqlParameter("@first_name", System.Data.SqlDbType.VarChar, 50);
                SqlParameter last_nameParam = new SqlParameter("@last_name", System.Data.SqlDbType.VarChar, 50);
                SqlParameter guardian_nameParam = new SqlParameter("@guardian_name", System.Data.SqlDbType.VarChar, 50);
                SqlParameter emailParam = new SqlParameter("@email", System.Data.SqlDbType.VarChar, 50);
                SqlParameter dobParam = new SqlParameter("@dob", System.Data.SqlDbType.Date, 3);
                SqlParameter phoneParam = new SqlParameter("@phone", System.Data.SqlDbType.VarChar, 50);
                SqlParameter street_addressParam = new SqlParameter("@street_address", System.Data.SqlDbType.VarChar, 50);
                SqlParameter cityParam = new SqlParameter("@city", System.Data.SqlDbType.VarChar, 50);
                SqlParameter stateParam = new SqlParameter("@state", System.Data.SqlDbType.VarChar, 50);
                SqlParameter zipParam = new SqlParameter("@zip", System.Data.SqlDbType.Int, 4);
                SqlParameter quotaParam = new SqlParameter("@quota", System.Data.SqlDbType.Int, 4);
                SqlParameter isAdultParam = new SqlParameter("@is_adult", System.Data.SqlDbType.Bit, 1);
                SqlParameter ethnicityParam = new SqlParameter("@ethnicity", System.Data.SqlDbType.VarChar, 50);
                SqlParameter restricted_to_techParam = new SqlParameter("@restricted_to_tech", System.Data.SqlDbType.Bit, 1);
                SqlParameter west_central_residentParam = new SqlParameter("@west_central_resident", System.Data.SqlDbType.Bit, 1);
                SqlParameter memberId_param = new SqlParameter("@memberId", System.Data.SqlDbType.Int, 4);

                first_nameParam.Value = toUpdate.first_name;
                last_nameParam.Value = toUpdate.last_name;
                guardian_nameParam.Value = (object)toUpdate.guardian_name ?? DBNull.Value;
                emailParam.Value = toUpdate.email;
                dobParam.Value = toUpdate.dob;
                phoneParam.Value = toUpdate.phone;
                street_addressParam.Value = toUpdate.street_address;
                cityParam.Value = toUpdate.city;
                stateParam.Value = toUpdate.state;
                zipParam.Value = toUpdate.zip;
                quotaParam.Value = toUpdate.quota;
                isAdultParam.Value = toUpdate.is_adult;
                ethnicityParam.Value = toUpdate.ethnicity;
                restricted_to_techParam.Value = toUpdate.restricted_to_tech;
                west_central_residentParam.Value = toUpdate.west_central_resident;
                memberId_param.Value = toUpdate.member_id;

                upcmd.Parameters.Add(first_nameParam);
                upcmd.Parameters.Add(last_nameParam);
                upcmd.Parameters.Add(guardian_nameParam);
                upcmd.Parameters.Add(emailParam);
                upcmd.Parameters.Add(dobParam);
                upcmd.Parameters.Add(phoneParam);
                upcmd.Parameters.Add(street_addressParam);
                upcmd.Parameters.Add(cityParam);
                upcmd.Parameters.Add(stateParam);
                upcmd.Parameters.Add(zipParam);
                upcmd.Parameters.Add(quotaParam);
                upcmd.Parameters.Add(isAdultParam);
                upcmd.Parameters.Add(ethnicityParam);
                upcmd.Parameters.Add(restricted_to_techParam);
                upcmd.Parameters.Add(west_central_residentParam);
                upcmd.Parameters.Add(memberId_param);

                upcmd.Prepare();

                upcmd.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}