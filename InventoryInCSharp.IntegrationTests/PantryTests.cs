// namespace InventoryInCSharp.IntegrationTests;
//
// [TestFixture]
// public class Pantry
// {
//     protected static readonly HttpClient client = new HttpClient();
//
//     public void InsertDirectlyToDatabase(Pantry newPantry)
//     {
//         using(var connection =
//               new MySqlConnection(
//                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
//         {
//             var sql = "INSERT INTO PantryList (pantryName) VALUES (@pantryName)";
//             var actual = connection.ExecuteAsync(sql,newPantry);
//             actual.Wait();
//         }   
//     }
//
// }

// public class GetByPrimaryKeyTest
// {
//     private static readonly HttpClient client = new HttpClient();
//
//     [SetUp]
//     public async void Setup()
//     {
//         try
//         {
//             using HttpResponseMessage httpResponse = await client.GetAsync("localhost:8000/api/Item/1");
//             httpResponse.EnsureSuccessStatusCode();
//             
//
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//
//     }
//     //Execute
//
//     [Test]
//     public void Test1()
//     {
//         Assert.Pass();
//     } 
// }