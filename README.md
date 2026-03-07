

Rows in my table: 365


## Running the Application

Follow these steps to run the ETL application:

### 1. Clone the repository

### 2. Download the CSV file

Download the dataset from the provided Google Drive link and place it in the project directory.

### 3. Set up the database

Run the SQL script located in the `sql` folder:

This will create the database and the required table.

### 4. Update the connection string

Open `Program.cs` and update the SQL Server connection string with your local database configuration.

Example:

Server=localhost;Database=TripDB;Trusted_Connection=True;

### 5. Build the project

dotnet build

### 6. Run the application

dotnet run

### 7. Check the results

After execution:

- Valid records will be inserted into the database table
- Duplicate records will be written to `duplicates.csv`



Assume your program will be used on much larger data files. Describe in a few sentences what you would change if you knew it would be used for a 10GB CSV input file.

For a 10GB file the main problem would be memory — right now I load everything 
into a List which would crash for large files.
I would change to streaming — read and insert rows in chunks without loading 
the whole file at once.
I would also increase the batch size for SqlBulkCopy to reduce database round-trips.
For duplicate detection, the HashSet approach won't work at that scale since it 
stores everything in RAM. I would probably handle duplicates on the database side 
using a staging table and SQL to find and remove them.
I would also add some kind of progress tracking so if something goes wrong 
the program doesn't have to start over from the beginning.
