# Baki
A small library help to simplify the creation of Windows Service application.

Unlike *windows service project*, Baki allows your application to be easily tested and debuged just like regular console application. Baki also embed the install/uninstall windows service without having to create a separate install project.

## How To Use Baki
##### Option 1. Manual Setup
1. Create a **console** project
2. Reference Baki.dll
3. Create a windows service host class

		class Host : IWindowsServiceHost
		{
			public void Start()
			{
				//your code goes here

				Console.Writeline("Service Started");
			}

			public void Stop()
			{
				Console.Writeline("Service Stopped");
			}
		}

4. Register host class in Program.cs

		class Program
		{
		    static void Main(string[] args)
		    {
		        WindowsService.Run<Host>(args);
		    }
		}

##### Option 2. Use NuGet
1. Create a **console** project
2. On Package Manager Console, type: 
     > install-package baki

3. Register host class in Program.cs

		class Program
		{
		    static void Main(string[] args)
		    {
		        WindowsService.Run<Host>(args);
		    }
		}

##Note
Baki is insipired by [TopShelf](http://topshelf-project.com/).  Topshelf is my recommended windows service framework if your application requires feature like [shelving](http://topshelf-project.com/documentation/shelving/) 

Baki (*bah key*), means small tray in [Manado](http://en.wikipedia.org/wiki/Manado) (dialek) language.
