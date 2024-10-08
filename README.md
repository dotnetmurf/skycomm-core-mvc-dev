# skycomm-core-mvc-dev

<h4>
    About the SkyComm Resource Portal
</h4>
<p>
    SkyComm is a conceptual company that provide leased intergated communication systems to fixed base operators
    at global airport locations. The SkyComm Resource Portal is a corporate intranet web application used by SkyComm employees
    for tracking resources and monitoring key performance indicators. The monitored resources are:
    <ul>
        <li>
            Airports served by SkyComm
        </li>
        <li>
            Communications equipment provided to customers or operated by SkyComm
        </li>
        <li>
            Corporate office location and their assigned personnel
        </li>
    </ul>
</p>
<h4>
    About the SkyCommCoreMVC web application project
</h4>
<p>
    The SkyCommCoreMVC web application is being developed as a demonstration project. It is a multi-tier application
    having layers for domain, data, services, and presentation. The dependency injection design pattern is used
    throughout the application.
    <ul>
        <li>
            The domain layer contains the data models that were created by using the Entity Framework Core Database First
            for SQL Server process.
        </li>
        <li>
            The data layer uses the repository design pattern to process the data represented by the domain models.
            Each repository class has a related interface.
        </li>
        <li>
            The service layer provides the data, and any needed business logic, to the presentation layer.
            Each service class has a related interface.
        </li>
        <li>
            The presentation layer uses the MVC and MVVM design patterns. Each controller uses a dedicated service
            for building the required view models, which are then passed to their related views.
        </li>
    </ul>
</p>
<p>
    The application is deployed to Azure as both a Web App and a Container App.
</p>
<p>
    Some of the technologies used in the application are:
    <ul>
        <li>
            Microsoft .NET Framework 7.0
        </li>
        <li>
            Microsoft ASP.NET Core 7.0
        </li>
        <li>
            Microsoft Entity Framework Core 7.0
        </li>
        <li>
            C#
        </li>
        <li>
            Razor
        </li>
        <li>
            SQL
        </li>
        <li>
            LINQ
        </li>
        <li>
            JSON
        </li>
        <li>
            Bootstrap / Bootswatch
        </li>
    </ul>
</p>
<p>
    The environment used to develop and host the application include:
    <ul>
        <li>
            Microsoft Visual Studio 2022 Community Edition
        </li>
        <li>
            Microsoft SQL Server Management Studio 18
        </li>
        <li>
            Microsoft Azure
        </li>
        <li>
            Docker
        </li>
        <li>
            Microsoft SQL Server Migration Assistant for Access
        </li>
        <li>
            Microsoft Visio
        </li>
        <li>
            LINQPad 7
        </li>
    </ul>
</p>
<p>
    The code repository for the project can be viewed
    <a href="https://github.com/dotnetmurf/skycomm-core-mvc-dev" target="_blank">here</a>.
</p>
<p>
    A video demonstration of the project can be viewed
    <a href="https://www.youtube.com/playlist?list=PL-c49941AOOupm18jSJnahq7T2jaGQ9Ga" target="_blank">here</a>.
</p>
<p>
    Please note that this version of the application is intended to only demonstrate viewing capabilities of the portal's data.
    The ability to create, edit, or delete data is not implemented in this version of the application.
</p>
