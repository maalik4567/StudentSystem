## Student System
-  .NET as Backend and Reactjs as Front end
-  ADO.NET with Dapper ORM ( where as different procedure being called)
- MS SQL
## **Important knowledge about enable CORS in .NET Framework v4.8**
- Go at path in your .NET Project at App_Start/WebApi.Config.cs
     - Enable the Cors writing this Line of Code
     - config.EnableCors();
- Secondly write a simple line of code or give Attribute above your Controller or any action method 
     - [EnableCorsAttribute("*","*","*")] // where as all three " " commas  be filled with * to share resource with any request for access 
       - public class StudentController : ApiController

![image](https://github.com/user-attachments/assets/6e923ca9-6990-46d3-86bd-28dcb7e7a2d8)
