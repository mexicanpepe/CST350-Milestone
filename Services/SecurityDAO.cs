using MySql.Data.MySqlClient;
using CST350_Minesweeper.Models; 

namespace CST350_Minesweeper.Services
{
    public class SecurityDAO
    {
        private string connectionString = "Server=192.168.0.214;Port=3306;Database=MinesweeperDB;User=root;Password=Qaz123wsx!;CharSet=utf8;";


        //checks email and password combo when attempting to login
        public User checkLogin(User user)
        {

            User currentUser = null;

            //assume no user if found
            //bool success = false;

            string sqlQuery = "SELECT * FROM Users WHERE Email = @email AND Password = @password";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@password", user.Password);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    
                    //if the reader has rows then the user email and password match within the DB
                    if (reader.HasRows)
                    {
                        //success = true;

                        //grab all properties from user
                        while (reader.Read())
                        {
                            currentUser = new User
                            {
                                UserID = Convert.ToInt32(reader["UserID"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Sex = reader["Sex"].ToString(),
                                Age = Convert.ToInt32(reader["Age"]),
                                State = reader["State"].ToString(),
                                Email = reader["Email"].ToString(),
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString()
                            };
                        }
                    }

                    reader.Close();
                }

                //errror handling
                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                }
            }


            //will return the user that was created with all properties if user email password combo is found
            //if not then the currentUser will return as null triggering a failure in the login.
            return currentUser;
        }

        //This checks if user exists in the DB by checking email
        public bool isCurrentUser(string email)
        {

            //assuming that the user does not exists in the DB
            bool userExists = false;

            //checking to see if the user input email exists
            string sqlStatement = "SELECT * FROM Users WHERE Email = @Email";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@Email", email.ToLower());

                try
                {
                    connection.Open();

                    MySqlDataReader reader = command.ExecuteReader();
                    userExists = reader.HasRows;

                    reader.Close();
                }
                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                }
            }



            return userExists;
        }


        //this method will add a user to the database based on registration form input
        //will be invoked upon successfull registration
        public void AddUser(User user)
        {
            string sqlQuery = "INSERT INTO Users (FirstName, LastName, Sex, Age, State, Email, Username, Password) " +
                                  "VALUES (@FirstName, @LastName, @Sex, @Age, @State, @Email, @Username, @Password)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                MySqlCommand command = new MySqlCommand(sqlQuery, connection);

                //parameters for what is being inserted into User table
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Sex", user.Sex);
                command.Parameters.AddWithValue("@Age", user.Age);
                command.Parameters.AddWithValue("@State", user.State);
                command.Parameters.AddWithValue("@Email", user.Email.ToLower());
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);


                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();

                    Console.WriteLine("New user added to the database successfully.");
                }

                //error handling message
                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                }
            }
        }
    }
}