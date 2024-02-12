这是一个支持连接MS SQL Server数据库，并且读取数据库服务器时间，将读取的时间同步设置到本地设备的软件。

![image](https://github.com/caocaofff/SQLDT/assets/67676342/a32fd15d-42bf-4dba-a118-32fdede268cc)

支持设置的配置：

<!--以下是配置服务器SQL Server数据库连接信息-->
    <add name="Data Source" connectionString="127.0.0.1"/>
    <add name="Initial Catalog" connectionString="projectdb"/>
    <add name="User Id" connectionString="sa"/>
    <add name="Password" connectionString="password"/>
  
    <!--定时器时间，单位：秒-->
    <add name="Timer" connectionString="1"/>

    <!--允许的时间误差，单位：秒-->
    <add name="Deviation" connectionString="10"/>
    
    <!--同步完时间后调用程序，留空表示不调用-->
    <add name="RunEXE" connectionString=""/>
