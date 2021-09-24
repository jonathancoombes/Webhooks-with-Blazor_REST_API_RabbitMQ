# Webhooks-with-Blazor_REST_API_RabbitMQ
.NET Core Project implementing Webhooks using Blazor | EF (MS SQL) | REST API | RabbitMq

<div style="border:1px sold; padding:5px">
 <a href="http://coombes.eastus2.cloudapp.azure.com/" >Click here or on the image to view a hosted instance of this project on Azure VM.<br><br>
  <img src="http://jonathancoombes.com/project.png" alt="SpaceX" /></a> 
 </div> 

 <h3 class="text-white">Webhook and RabbitMQ REST API Demonstration</h3>
 <p class="">Welcome to the .NET demonstration project.</p>
<strong class="">Use-Case</strong>
<p>
This application allows for the registration of "webhooks" at the Airline by interested subscribers(Travel Agents) to provide them with access to data
change notifications made available by the publisher(Airline). Once a webhook is registered, the subscribers will be notified of changes to current data (price changes etc) once such an event has occurred.
</p>
<strong class="">Technology</strong>
<p>
.NET Core as the main back-end engine that will host the RESTFull API endpoints and deliver the event-driven information via a message broker application(RabbitMQ).
Data will be persisted by a SQL server hosted on a Azure VM.
The front end user interfaces are created using Blazor Wasm and hosted by a .NET Core server application.
</p>
<h4>This solution is based on 3 applications:</h4>
<ol>
 
<strong><li> Client: Blazor Application</li></strong>
  <p> User interface for webhook registration and testing.</p>

<strong><li> Server: .Net Core Web Api Application</li></strong>
<p> Webhook Registration API and RabbitMq Host application.</p>

<strong><li> AirLineSendAgent .Net Core Console Application</li></strong>
<p> Event driven webhook delivery application.</p>
 
 </ol><hr>
<p style="color:green"> ((-> Thank you for taking the time to check out this project! <-)) </p>
