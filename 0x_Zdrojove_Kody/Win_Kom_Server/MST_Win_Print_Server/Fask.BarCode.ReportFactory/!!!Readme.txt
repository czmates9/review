1)=================
https://social.msdn.microsoft.com/Forums/en-US/05248bbb-c4a2-49d6-8659-f8b863c2388a/error-while-loading-code-module?forum=vsreportcontrols
In order to compile an RDLC that uses a custom assembly, the assembly should be in the %PROGRAMFILES%\Visual Studio 8\Common7\IDE\PublicAssemblies or PrivateAssemblies folder.
To diagnose assembly loading problems, you can use the Fusion log viewer tool (fuslogvw.exe). 


2)=================
https://blogs.msdn.microsoft.com/mosharaf/2005/12/20/adding-custom-code-to-local-reports-in-visual-studio-net-2005-problems-solutions/
Adding custom code to Local Reports in Visual Studio.NET 2005 (Problems & Solutions)
★★★★★★★★★★★★★★★
MohamedDecember 20, 200570
0
0
0
If you are one of the people who used and enjoyed SQL Server Reporting Services (SSRS) in SQL 2000 and you wanted to use it in your windows/web applications without the server side components (just like what you are doing with Crystal Reports). So you would be much exited about the new ReportViewer Controls. ReportViewer controls are two controls for Windows and Web applications that can either show reports produced by SQL Server Reporting Services or process and render reports locally.

 

These reports are called Local Report and it’s intended to provide reporting features without SSRS. You can use them without having a license for SSRS but be aware that you would lose some features like


Report parameters in client report definitions (.rdlc) do not map to query parameters. There is no parameter input area in a client report definition that accepts values that are subsequently used in a query.

Client report definitions do not include embedded query information. You must define data sources that return ready-to-use data for the report. So you have to provide a DataSource for your report like a DataSet, DataTable…

Browser-based printing through the RSClientPrint ActiveX control is not available for client report definitions that run in the ReportViewer Web server control. The print control is part of the report server feature set.

You would loose all collaboration activities of SSRS like scheduling to send reports to some users at specific time

 

I used ReportViewer for Windows forms in small report in which I had to add custom code in it and I had some problems that I would like to share with you solutions for them.

First we would go quickly through the process of creating simple report as following:

1)      Create a new windows forms project and open its form. Drag the ReportViewer control from the Data tab as shown in the image below

2)      Use the smart tag tasks to dock the report in all the form (you can use properties window as well). Then from the smart tag choose “Design new report”. If you didn’t see this option you can simply create new report. From solution explorer, right click on the project name then choose “add new item” then choose report.

3)      The report file would have the extension “rdlc” not “rdl” as it would be in server reports.

4)      Local Reports need to have a data source so go to Data Sources window and create your data source (let’s say that it will get all Category name and description from categories table in Northwind)

5)      From the ToolBar window, drop a table inside the report

6)      From the Data Source window, drag & drop the CategoryName and Description fields to the details row of the table. Now you finished creating the report. Let’s bind it to the ReportViewer

7)      Return back to the ReportViewer and from the smart tag menu choose, select your report name from the drop down menu.

8)      Run the application.

 

Now you have the report. Let’s go to add custom code to the report.

 

Why would you add custom code?

You may want to add custom code to the report to do more actions than what’s provided with the report functions. So it gives you a base for extending your report. You may want to add custom code to implement simple string manipulation task or sophisticated data access manipulation.

 

Cod added to Local Report can be one of two types:

            Embedded Code: Added directly inside the rdlc file. If you open the file in notepad, you can see the code inside the <Code> tag.

            Custom Assemblies: You can add a reference in external assembly and use functions inside it.

 

How to add a custom code in a local report?

According to MSDN library, you add it as following:

Embedded Code:


On the Report menu, click Report Properties.

On the Code tab, in Custom code, type the code.

Custom Assemblies:


On the Report menu, click Report Properties.

On the References tab, do the following:

In References, click the add (…) button and then select or browse to the assembly from the Add Reference dialog box.

In Classes, type name of the class and provide an instance name to use within the report. That’s in the case of instance members, in the case of static (shared in VB) members, you don’t need to add anything in Classes.

 

What’s the code that I can add?

You can add class members only (properties, methods, fields) but you can’t add classes because these members are encapsulated in a class at runtime, this class called Code class.

 

Take a look at this code snippet.

 

Public ReadOnly Property getSomeData() As String

        Get

            Return sharedMember

        End Get

    End Property

 

    Dim sharedMember As String = “Omar”

 

    Public Function toUpperCase(ByVal s As String)

        Dim conn As New System.Data.SqlClient.SqlConnection()

        Return s.toUpper()

    End Function

 

You can use this code inside a textBox or a table column as following =Code.toUpperCase(Code.getSomeData)

 

What are the namespaces that I can use inside my custom code?

All .NET namespaces can be used inside your custom code but be aware that by default only Microsoft.VisualBasic, System.Convert, and System.Math namespaces are referenced inside your reporting custom code.

 

Now let’s jump into the problems.

 

Adding code that uses types that are not in these namespaces (Microsoft.VisualBasic, System.Convert, and System.Math)

Let’s say that you are using a code that access SQL Server database to get some data to use it inside your report. You are using SqlConnection in your code. You would receive an error like this

There is an error on line 9 of custom code: [BC30002] Type ‘System.Data.SqlClient.SqlConnection’ is not defined.

Because System.Data.SqlClient resides in the System.Data.dll assembly and this assembly is not referenced by default by the report custom code. To solve this problem you need to add a reference to the System.Data.dll assembly in the References tag in the report properties window.

But this is not the end of the story; you would face another problem which described in the next step

 

How to make an assembly trusted for your report code

Because Local Reports like SQL Server Reporting Services reports are written using open format (Report Definition Language (RDL)). Any user can add any code or reference any assembly from your rdl file at the production environment which would cause a BIG security hole. You need to mark these assemblies as trusted inside your Windows Application code. Or you would get an error like this

The report references the code Module ‘System.Data, Version=2.0.0.0, Culture=neutral, Publickey Token=b77a5XXXXXXXX’, which is not a trusted assembly

 

You need to use the AddTrustedCodeModuleInCurrentAppDomain method of the LocalReport class. You can add the following line at the Form_load event handler of your windows application.

 

this.reportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(“System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089”);

 

And again it’s not the end of the story; you will face another problem ;). Please be patient with me because I faced all these problems sequentially and I didn’t find any resources to help me.

 

What are the Code Access Security (CAS) permissions assigned to any code running inside LocalReport by default

By default the only permission bound to the code running inside the LocalReport is ExecutePermission So your code can’t access file system, databases, registry… unless you give it explicitly the required permissions.

For example, if you are trying to connect to a database from your code, you will get this error

Request of the permission of type ‘System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.0.0, Culture=neutral, PublicKey Token=b77a5c561934e089’ failed.

 

To give the assemblies running in the report the appropriate permissions, you need to use the ExecuteReportInCurrentAppDomain Method of the LocalReport class. Which causes the expressions in the report to be executed inside the current appDomain instead of executing them in a sandbox.

 

The ExecuteReportInCurrentAppDomain method takes one parameter of the type Evidence.

In the following example I’m using the evidence associated with my current appDomain so the report code will have all permissions that I have in my application

 

this.reportViewer1.LocalReport.ExecuteReportInCurrentAppDomain(AppDomain.CurrentDomain.Evidence);

 

By working on these steps, your code will run smoothly inside your report.

 

 

For the people who are trying to move their code in separated assembly and reference it from their report, they would face another problem.

If you are following the steps mentioned in MSDN regarding how to reference custom code (mentioned above). You will end up with this message

Error while loading code module: ‘ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null’. Details: Could not load file or assembly ‘ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null’ or one of its dependencies. The system cannot find the file specified.

 

After a lot of trials with this error, I found that I have to do the following.


Select the report file from Solution Explorer, Go to Properties Window and change its BuildAction to Content

Set Copy to output Directory property to Copy always or Copy if newer

In the form designer class (the partial class created by Visual Studio according to naming schema <FormName>.Designer.CS or <FormName>.Designer.VB) replace the line which instruct the ReportViewer to load the report file from resource file with another line that instruct the ReportViewer to load the report from current directory.

So this line

this.reportViewer1.LocalReport.ReportEmbeddedResource = “testLocalReport.Report1.rdlc”;


With this line

this.reportViewer1.LocalReport.ReportPath = “Report1.rdlc”;


Copy the dll that you are referencing to the Debug or Release directory of your application

Make the line that executes the report in the current appDomain first, then add the lines that trust the referenced assembly and all assemblies referenced by this assembly (see above)

 

I hope this would help you to really enjoy VS 2005 features

3)=======================
http://devcoma.blogspot.cz/2010_05_01_archive.html

Migrating external assemblies used in RDLC from VS2005 to VS2010
If you are using ReportViewer and RDLC reports in visual studio 2005 you may use externals assemblies for additional rules and formulas, in order to enable external assemblies you need to call the AddTrustedCodeModuleInCurrentAppDomain function.
rv.LocalReport.AddTrustedCodeModuleInCurrentAppDomain("MyLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

Porting this code  to VS2010 without changes will generate the exception:

Exception of type 'System.Web.HttpException' was thrown.
Exception of type 'System.Web.HttpUnhandledException' was thrown.
Report execution in the current AppDomain requires Code Access Security policy, 
which is off by default in .NET 4.0 and later. 
Enable legacy CAS policy or execute the report in the sandbox AppDomain.

With the exception detail:

Exception of type 'System.Web.HttpException' was thrown.
Exception of type 'System.Web.HttpUnhandledException' was thrown.
at System.Web.UI.Page.HandleError(Exception e) 
at System.Web.UI.Page.ProcessRequestMain(Boolean includeStagesBeforeAsyncPoint, Boolean includeStagesAfterAsyncPoint) 
at System.Web.UI.Page.ProcessRequest(Boolean includeStagesBeforeAsyncPoint, Boolean includeStagesAfterAsyncPoint) 
at System.Web.UI.Page.ProcessRequest() 
at System.Web.Util.AspCompatApplicationStep.System.Web.HttpApplication.IExecutionStep.Execute() 
at System.Web.HttpApplication.ExecuteStep(IExecutionStep step, Boolean& completedSynchronously) 
Exception of type 'System.Web.HttpException' was thrown.
Exception of type 'System.Web.HttpUnhandledException' was thrown.
Report execution in the current AppDomain requires Code Access Security policy, which is off by default in .NET 4.0 and later. Enable legacy CAS policy or execute the report in the sandbox AppDomain.
at Microsoft.Reporting.WebForms.LocalReport.ReportRuntimeSetupHandler.AddTrustedCodeModuleInCurrentAppDomain(String assemblyName) 
at Microsoft.Reporting.WebForms.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(String assemblyName) 
at Web.Report.OnInit(EventArgs e) in C:\inetpub\wwwroot\Web\Report.aspx.cs:line 500 
at System.Web.UI.Control.InitRecursive(Control namingContainer) 
at System.Web.UI.Page.ProcessRequestMain(Boolean includeStagesBeforeAsyncPoint, Boolean includeStagesAfterAsyncPoint)


The solution is to use the new AddFullTrustModuleInSandboxAppDomain function, using the SetBasePermissionsForSandboxAppDomain function, here is an example:



using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
//...
//... 
PermissionSet permissions = new PermissionSet(PermissionState.None);
permissions.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
rv.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);
Assembly asm = Assembly.Load("MyLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
AssemblyName asm_name = asm.GetName();
rv.LocalReport.AddFullTrustModuleInSandboxAppDomain(new StrongName(new StrongNamePublicKeyBlob(asm_name.GetPublicKeyToken()), asm_name.Name, asm_name.Version));

Hope it helps!, and happy coding!