using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;


namespace evg
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
       
            //check connect database
            try
            {
                System.Collections.Generic.List<Agent> list = bdagent.GetContext().Agent.ToList();
                //Connect database
                SqlConnection connection = new SqlConnection("Data Source=iyujghw4585y.ddns.net;Initial Catalog=test2;initial catalog=test2;persist security info=True;user id=user ;password=user");
                //print sale  in year
                foreach (Agent item in list)
                {
                    SqlCommand sqlcmd = new SqlCommand($"(SELECT SUM(ProductCount) FROM dbo.ProductSale WHERE AgentID = {item.ID} AND YEAR(dbo.ProductSale.SaleDate) = {Convert.ToInt32(DateTime.Now.Year)} )", connection);
                    connection.Open();
                    item.PtdSale = sqlcmd.ExecuteScalar().ToString();
                    connection.Close();
                    

               
                }
                foreach (Agent item in list)
                {
                    SqlCommand sqlcmd = new SqlCommand($"(SELECT Logo FROM dbo.Agent)", connection);




               



                    connection.Open();
                    item.imgpath = sqlcmd.ExecuteScalar().ToString();
                    connection.Close();
                    


                }
                //calculate discount 
                try
                {
                    foreach (Agent y in list)
                    {

                        SqlCommand sqlcmd = new SqlCommand($"(SELECT SUM(ProductCount) FROM dbo.ProductSale WHERE AgentID = {y.ID})", connection);
                        connection.Open();
                        int i = Convert.ToInt32(sqlcmd.ExecuteScalar().ToString());
                        connection.Close();
                        if (i > 500000)
                        {
                            y.skidka = "25";

                        }
                        else if (i > 149999)
                        {
                            y.skidka = "20";
                        }

                        else if (i > 49999)
                        {
                            y.skidka = "10";
                        }
                        else if (i > 4999)
                        {
                            y.skidka = "5";
                        }

                        else if (i > 9999)
                        {
                            y.skidka = "0";

                        }

                        else
                        {
                            y.skidka = "0";
                        }




                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Не у всех агентов присутствуют актуальные записи продаж в бд", "Предупреждение", MessageBoxButton.OK);
                }
                lst.ItemsSource = list;
            }
            catch (Exception)
            {
                MessageBox.Show("Отсутствует доступ к БД", "Ошибка", MessageBoxButton.OK);
            }

          







        }
    }
}
