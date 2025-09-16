This is a template management application.

To run the application, you must have Docker installed on your computer.

Please follow the steps below to run the program:

1) You need to download the Zip folder with the code of this repository;
2) Unzip the folder;
3) In the program folder, you need to open a command line, make sure that the open command line contains the path to the program folder.
4) Write the following query: docker compose up --build;
5) After that, open the following link in your browser: http://localhost:5119/swagger/index.html;
6) After testing the program, press Ctrl+C to stop the program;
7) Write the following query: docker-compose down 

Information for quick test:

Content: <html>\n  <body>\n    <h1>Hello, {{UserName}}!</h1>\n    <p>Thank you for your order <b>№{{OrderNumber}}</b> from {{OrderDate}}.</p>\n    <p>We will deliver it to your address: {{Address}}.</p>\n    <br/>\n    <p>Sincerely,<br/>Store Team</p>\n  </body>\n</html>
JSON:{
  "UserName": "Peter",
  "OrderNumber": "513",
  "OrderDate": "15.09.2025",
  "Address": "Odessa"
}
