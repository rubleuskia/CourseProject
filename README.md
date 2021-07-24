## Scripts usage
0. Open powershell in root folder

1. Add migration with name "SomeMigrationName"
`.\migrations.ps1 add SomeMigrationName`

2. Remove the last migration (not applied to database)
`.\migrations.ps1 remove`

3. Update database based on migrations
`.\updateDb.ps1