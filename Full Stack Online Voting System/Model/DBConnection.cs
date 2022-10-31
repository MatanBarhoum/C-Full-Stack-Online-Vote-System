using System.Data.SqlClient;

namespace Full_Stack_Online_Voting_System.Model
{
    public class DBConnection
    {
        private string connection_string = "Data Source=sql.bsite.net\\MSSQL2016;Initial Catalog=matanbar_VotingSystem;Persist Security Info=True;User ID=matanbar_VotingSystem;Password=123123aaa;MultipleActiveResultSets=True";
        public int AuthenticateUser(Student student)
        {
                using (SqlConnection conn = new SqlConnection(connection_string))
                {
                    try
                    {
                        conn.Open();
                        var command = "SELECT studentid, password from students WHERE studentid = @studentid and password = @password";

                        using (SqlCommand cmd = new SqlCommand(command, conn))
                        {
                            cmd.Parameters.AddWithValue("@studentid", Convert.ToInt32(student.StudentID));
                            cmd.Parameters.AddWithValue("@password", student.Password);

                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) == Convert.ToInt32(student.StudentID) && reader.GetString(1) == student.Password)
                                    {
                                        return 1;
                                    }
                                }
                            }
                        }
                    }
                catch (Exception err) { Console.WriteLine(err); }
                finally { conn.Close(); }
                return 0;
                }
        } 
        public bool HasVoted(int id)
        {
            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                try
                {
                    conn.Open();
                    var command = "SELECT hasvoted FROM students WHERE studentid = @studentid";

                    using (SqlCommand cmd = new SqlCommand(command, conn))
                    {
                        cmd.Parameters.AddWithValue("@studentid", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.GetInt32(0) == 1)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                catch (Exception err) { Console.WriteLine(err); }
                finally { conn.Close(); }
            }
            return false;
        }
        public int Vote(int id, int studentID)
        {
            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                try
                {
                    conn.OpenAsync();
                    var command = "UPDATE competitors SET votes = votes + 1 WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(command, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQueryAsync();
                    }

                    var updateHasVotedCMD = "UPDATE students SET hasvoted = 1 WHERE studentid = @studentid";

                    using (SqlCommand cmd1 = new SqlCommand(updateHasVotedCMD, conn))
                    {
                        cmd1.Parameters.AddWithValue("@studentid", studentID);
                        cmd1.ExecuteNonQueryAsync();
                        return 1;
                    }

                }
                catch (Exception err) { Console.WriteLine(err); }
                finally { conn.CloseAsync(); }
            }
            return 0;
        }
    }
}
