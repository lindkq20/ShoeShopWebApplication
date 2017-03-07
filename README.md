# ShoeShopWebApplication
Prueba mantenimiento Super Zapato

En el Web.config del proyecto ShoeShopWebApi
Configurar el string de conexión a la base de datos que se va a utilizar, en la seccion de "connectionStrings", donde dice "BDShoeShopEntities".
<connectionStrings>
    <add name="BDShoeShopEntities" connectionString="metadata=res://*/ModelShoeShop.csdl|res://*/ModelShoeShop.ssdl|res://*/ModelShoeShop.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=192.168.100.14;initial catalog=BDShoeShop;persist security info=True;user id=sa;password=123456;multipleactiveresultsets=True;application name=EntityFramework&quot;" providerName="System.Data.EntityClient" />
</connectionStrings>

En el Web.config del proyecto ShoeShopWebApi, configurar la url de los servicios en la sección de "appSettings", en el parametro "BaseUrlService".
  <appSettings>
    <add key="webpages:Version" value="3.0.0.0" />
    <add key="webpages:Enabled" value="false" />
    <add key="ClientValidationEnabled" value="true" />
    <add key="UnobtrusiveJavaScriptEnabled" value="true" />
    <add key="BaseUrlService" value="http://localhost:52257/" />
  </appSettings>