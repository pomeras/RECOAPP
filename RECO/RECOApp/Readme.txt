Installing

Just create a blank MS SQL database and update the connection string in the appsettings.json to point to the database with the correct credentials. 

Example below:

"ConnectionStrings": {
    "RECOAppContext": "Data Source=SIMON;Initial Catalog=RECO;User ID=sa;Password=password12",
  }

Open "\RECOApp\RECOApp\Data\RECO_db.sql" file and run the query to create two tables: Document and Page inside REPO DB.
