using System.Collections.Generic;
using Infrastructure.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;

#nullable disable

namespace Domain.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.InsertData(
                "ConfigurableCategories",
                new[] { "Name", "Description", "Position"},
                new object[] { "Baseic", "Baseic", 1});
            
            #region Setup View
            
            var sql = $"SET @userId = (SELECT Id FROM AspNetUsers WHERE UserName='adminWolbergPro' limit 1);";
            sql += $"Insert into ConfigurableUserViews (UserId, Name, Description, IsShareAble, IsDefault, Color, Settings, IsPredefined) VALUES (@userId, 'Base View Of Users list', '', 1, 1, '#000000', '[]', 1),";
            sql += $"(@userId, 'Base View Of Client Contacts list', '', 1, 1, '#000000', '[]', 1),";
            sql += $"(@userId, 'Base View Of Clients list', '', 1, 1, '#000000', '[]', 1),";
            sql += $"(@userId, 'Base View Of Activity Logs list', '', 1, 1, '#000000', '[]', 1);";
            migrationBuilder.Sql(sql);
            #endregion
            
            #region ActivityLogs View
            var metaData = new Dictionary<string, string>
            {
                { "width", "30%" }
            };
            string metaDataJson = JsonConvert.SerializeObject(metaData);
            sql = $"SET @categoryId = (SELECT Id FROM ConfigurableCategories WHERE Name = 'Baseic');";
            sql += $"INSERT INTO ConfigurableEntityColumnDefinitions (EntityName, ColumnName, FieldAlias, FieldPath, DisplayName, Description, CategoryId, Permissions, MetaData, DataType, FilterOperationType, IsSortable, IsFilterable, IsVirtualColumn) VALUES ";
            sql += $"('ActivityLogs', 'Id', 'id', 'id', 'Activity Log Id', 'Entity Id', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.Number}, {(int)EntityColumnOperationType.IdFilterField}, 1, 1, 0),";
            sql += $"('ActivityLogs', 'OwnerUserId','ownerUserId', 'user.id', 'Owner User Id', 'Owner User Id', @categoryId, '[\"Admin\",\"IT\"]', '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1, 0),";
            sql += $"('ActivityLogs', 'OwnerUserEmail','ownerUserEmail', 'user.email', 'Owner User Email', 'Owner User Email', @categoryId, '[\"Admin\",\"IT\"]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1, 1),";
            sql += $"('ActivityLogs', 'OwnerUserFirstName','ownerUserFirstName', 'user.firstName', 'Owner User First name', 'Owner User first name', @categoryId, '[\"Admin\",\"IT\"]', '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1, 1),";
            sql += $"('ActivityLogs', 'OwnerUserLastName','ownerUserLastName', 'user.lastName', 'Owner User Last name', 'Owner User last name', @categoryId, '[\"Admin\",\"IT\"]', '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1, 1),";
            sql += $"('ActivityLogs', 'UserId', 'userId', 'user.id', 'User Id', 'User Id', @categoryId, '[]', '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1, 1),";
            sql += $"('ActivityLogs', 'userEmail', 'userEmail', 'user.email', 'User Email', 'User Email', @categoryId, '[]', '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1, 1),";
            sql += $"('ActivityLogs', 'userFirstName', 'userFirstName', 'user.firstName', 'User First name', 'User first name', @categoryId, '[]', '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1, 1),";
            sql += $"('ActivityLogs', 'userLastName', 'userLastName', 'user.lastName', 'User last name', 'User last name', @categoryId, '[]', '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1, 1),";
            sql += $"('ActivityLogs', 'EntityId', 'entityId', 'entityId', 'Entity Id', 'Entity Id', @categoryId, '[]', '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1, 0),";
            sql += $"('ActivityLogs', 'EntityType', 'entityType', 'entityType', 'Entity Type', 'Entity Type', @categoryId, '[]', '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1, 0),";
            sql += $"('ActivityLogs', 'OperationType', 'operationType', 'operationType', 'Operation Type', 'Operation Type', @categoryId, '[]', '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1, 0),";
            sql += $"('ActivityLogs', 'Activity', 'activity', 'activity', 'Activity', 'Activity', @categoryId, '[]', '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 0, 1, 0),";
            sql += $"('ActivityLogs', 'Details', 'details', 'details', 'Details', 'Details', @categoryId, '[]', '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 0, 1, 0),";
            sql += $"('ActivityLogs', 'Status', 'status', 'status', 'Status', 'Status', @categoryId, '[]', '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1, 0),";
            sql += $"('ActivityLogs', 'IpAddress', 'ipAddress', 'ipAddress', 'Ip Address', 'Ip Address', @categoryId, '[]', '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1, 0),";
            sql += $"('ActivityLogs', 'UserAgent', 'userAgent', 'userAgent', 'User Agent', 'User Agent', @categoryId, '[]', '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 0, 1, 0),";
            sql += $"('ActivityLogs', 'CreatedAt', 'createdAt', 'createdAt', 'Created At', 'Created At', @categoryId, '[]', '{metaDataJson}', {(int)EntityColumnDataType.Date}, {(int)EntityColumnOperationType.DateFilterField}, 1, 1, 0);";
            migrationBuilder.Sql(sql);
            
            
            sql = $"SET @viewId = (SELECT Id FROM ConfigurableUserViews WHERE Name = 'Base View Of Activity Logs list');";
            sql += $"INSERT INTO ConfigurableUserViewHasConfigurableEntityColumnDefinitions (ConfigurableUserViewId, ConfigurableEntityColumnDefinitionId, Position) VALUES ";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ActivityLogs' and ColumnName = 'Id'), 1),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ActivityLogs' and ColumnName = 'OwnerUserId'), 2),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ActivityLogs' and ColumnName = 'UserId'), 3),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ActivityLogs' and ColumnName = 'EntityId'), 4),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ActivityLogs' and ColumnName = 'EntityType'), 5),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ActivityLogs' and ColumnName = 'OperationType'), 6),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ActivityLogs' and ColumnName = 'Activity'), 7),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ActivityLogs' and ColumnName = 'Details'), 8),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ActivityLogs' and ColumnName = 'Status'), 9),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ActivityLogs' and ColumnName = 'IpAddress'), 10),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ActivityLogs' and ColumnName = 'UserAgent'), 11),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ActivityLogs' and ColumnName = 'CreatedAt'), 12);";
            migrationBuilder.Sql(sql);
            
            #endregion
            
            #region Client View
            
            sql = $"SET @categoryId = (SELECT Id FROM ConfigurableCategories WHERE Name = 'Baseic');";
            sql += $"INSERT INTO ConfigurableEntityColumnDefinitions (EntityName, ColumnName, FieldAlias, FieldPath, DisplayName, Description, CategoryId, Permissions, MetaData, DataType, FilterOperationType, IsHidden, IsSortable, IsFilterable, IsVirtualColumn) VALUES ";
            sql += $"('Clients', 'Id', 'Client Id', 'id', 'id', 'Client Id', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.Number}, {(int)EntityColumnOperationType.IdFilterField},0 , 1, 1, 0),";
            sql += $"('Clients', 'ParentId', 'Client Parent Id', 'id', 'id', 'Client Parent Id', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 0, 1, 0),";
            sql += $"('Clients', 'OwnerUserId', 'Client Owner User Id', 'id', 'id', 'User Owner of the clientId', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 1, 1),";
            sql += $"('Clients', 'Name', 'Client Name', 'Client Name', 'id', 'id', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 1, 0),";
            sql += $"('Clients', 'Description', 'Client Description', 'id', 'id', 'Description', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 0, 1, 0),";
            sql += $"('Clients', 'Website', 'Client Website', 'id', 'id', 'Client Website', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 0, 0),";
            sql += $"('Clients', 'CountryId', 'Client Country Id', 'countryId', 'country.id', 'Client Country Id', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.Number}, {(int)EntityColumnOperationType.IdFilterField},0 , 1, 1, 1),";
            sql += $"('Clients', 'CountryId', 'Client Country Name', 'countryName', 'country.countryName', 'Client Country Id', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.Number}, {(int)EntityColumnOperationType.IdFilterField},0 , 1, 1, 1),";
            sql += $"('Clients', 'Address', 'Client Address', 'address', 'address', 'Client Address', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 1, 0),";
            sql += $"('Clients', 'City', 'Client City', 'city', 'city', 'Client City', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 1, 0),";
            sql += $"('Clients', 'CreatedAt', 'Client Created At', 'createdAt', 'createdAt', 'Client Created At', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.Date}, {(int)EntityColumnOperationType.DateFilterField},0 , 1, 1, 0);";
            
            migrationBuilder.Sql(sql);
            
            sql = $"SET @viewId = (SELECT Id FROM ConfigurableUserViews WHERE Name = 'Base View Of Clients list');";
            sql += $"INSERT INTO ConfigurableUserViewHasConfigurableEntityColumnDefinitions (ConfigurableUserViewId, ConfigurableEntityColumnDefinitionId, Position) VALUES ";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'Id'), 1),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'ParentId'), 2),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'OwnerUserId'), 3),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'Name'), 4),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'Description'), 5),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'Website'), 6),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'Address'), 7),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'City'), 8),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'CreatedAt'), 9);";
            migrationBuilder.Sql(sql);
            
            #endregion
            
            #region ClientContacts View
            
            sql = $"SET @categoryId = (SELECT Id FROM ConfigurableCategories WHERE Name = 'Baseic');";
            sql += $"INSERT INTO ConfigurableEntityColumnDefinitions (EntityName, ColumnName, FieldAlias, FieldPath, DisplayName, Description, CategoryId, Permissions, MetaData, DataType, FilterOperationType, IsHidden, IsSortable, IsFilterable, IsVirtualColumn) VALUES ";
            sql += $"('ClientContacts', 'Id', 'Client Contact Id', 'id', 'id', 'Entity Id', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.Number}, {(int)EntityColumnOperationType.IdFilterField}, 0, 1, 1, 0),";
            sql += $"('ClientContacts', 'ClientId', 'Client Id', 'clientId', 'clientId', 'Client Id', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.Number}, {(int)EntityColumnOperationType.IdFilterField},0 , 0, 0, 0),";
            sql += $"('ClientContacts', 'FirstName', 'Client Contact First Name', 'firstName', 'firstName', 'Client Contact First Name', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 1, 0),";
            sql += $"('ClientContacts', 'LastName', 'Client Contact Last Name', 'lastName', 'lastName', 'Client Contact Last Name', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 1, 0),";
            sql += $"('ClientContacts', 'Email', 'Client Contact Email', 'email', 'email', 'Client Contact Email', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1, 0),";
            sql += $"('ClientContacts', 'Phone1', 'Client Contact Phone 1', 'phone1', 'phone1', 'Client Contact Phone 1', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1, 0),";
            sql += $"('ClientContacts', 'Phone2', 'Client Contact Phone 2', 'phone2', 'phone2', 'Client Contact Phone 2', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1, 0),";
            sql += $"('ClientContacts', 'Fax', 'Client Contact Fax', 'fax', 'fax', 'Client Contact Fax', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 0, 0, 0),";
            sql += $"('ClientContacts', 'Whatsapp', 'Client Contact Whatsapp', 'whatsapp', 'whatsapp', 'Client Contact Whatsapp', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 0, 0, 0),";
            sql += $"('ClientContacts', 'CountryId', 'Client Contact Country Id', 'countryId', 'country.id', 'Client Contact Country Id', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.Number}, {(int)EntityColumnOperationType.IdFilterField},1 , 1, 0, 1),";
            sql += $"('ClientContacts', 'CountryName', 'Client Contact Country Name', 'countryName', 'country.countryName', 'Client Contact Country name', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.Number}, {(int)EntityColumnOperationType.IdFilterField},1 , 1, 0, 1),";
            sql += $"('ClientContacts', 'PostalCode', 'Client Contact Postal Code', 'postalCode', 'postalCode', 'Client Contact Postal Code', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 0, 0, 0),";
            sql += $"('ClientContacts', 'Address', 'Client Contact Address', 'address', 'address', 'Client Contact Address', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1, 0),";
            sql += $"('ClientContacts', 'City', 'Client Contact City', 'city', 'city', 'Client Contact City', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1, 0),";
            sql += $"('ClientContacts', 'State', 'Client Contact State', 'state', 'state', 'Client Contact State', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 0, 1, 0),";
            sql += $"('ClientContacts', 'JobTitle', 'Client Contact Job Title', 'jobTitle', 'jobTitle', 'Client Contact Job Title', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 ,0, 1, 0),";
            sql += $"('ClientContacts', 'Department', 'Client Contact Department', 'department', 'department', 'Client Contact Department', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 0, 1, 0),";
            sql += $"('ClientContacts', 'Company', 'Client Contact Company', 'company', 'company', 'Client Contact Company', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 0, 0),";
            sql += $"('ClientContacts', 'Notes', 'Client Contact Notes', 'notes', 'notes', 'Client Contact Notes', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 0, 0, 0),";
            sql += $"('ClientContacts', 'CreatedAt', 'Client Contact Created At', 'createAt', 'createAt', 'Client Contact Created At', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.Date}, {(int)EntityColumnOperationType.DateFilterField},1 , 1, 1, 0);";
            
            migrationBuilder.Sql(sql);
            
            sql = $"SET @viewId = (SELECT Id FROM ConfigurableUserViews WHERE Name = 'Base View Of Client Contacts list');";
            sql += $"INSERT INTO ConfigurableUserViewHasConfigurableEntityColumnDefinitions (ConfigurableUserViewId, ConfigurableEntityColumnDefinitionId, Position) VALUES ";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ClientContacts' and ColumnName = 'Id'), 1),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ClientContacts' and ColumnName = 'ClientId'), 2),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ClientContacts' and ColumnName = 'FirstName'), 3),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ClientContacts' and ColumnName = 'LastName'), 4),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ClientContacts' and ColumnName = 'Email'), 5),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ClientContacts' and ColumnName = 'Phone1'), 6),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ClientContacts' and ColumnName = 'Phone2'), 7),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ClientContacts' and ColumnName = 'Fax'), 8),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ClientContacts' and ColumnName = 'Whatsapp'), 9),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ClientContacts' and ColumnName = 'CountryId'), 10),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ClientContacts' and ColumnName = 'PostalCode'), 11),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ClientContacts' and ColumnName = 'Address'), 12),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ClientContacts' and ColumnName = 'City'), 13),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ClientContacts' and ColumnName = 'State'), 14),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ClientContacts' and ColumnName = 'JobTitle'), 15),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ClientContacts' and ColumnName = 'Department'), 16),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ClientContacts' and ColumnName = 'Company'), 17),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ClientContacts' and ColumnName = 'Notes'), 18),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ClientContacts' and ColumnName = 'CreatedAt'), 19);";
            migrationBuilder.Sql(sql);
            
            #endregion
            
            #region Users list View
            
            sql = $"SET @categoryId = (SELECT Id FROM ConfigurableCategories WHERE Name = 'Baseic');";
            sql += $"INSERT INTO ConfigurableEntityColumnDefinitions (EntityName, ColumnName, FieldAlias, FieldPath, DisplayName, Description, CategoryId, Permissions, MetaData, DataType, FilterOperationType, IsHidden, IsSortable, IsFilterable, IsVirtualColumn) VALUES ";
            sql += $"('ApplicationUser', 'Id', 'id', 'id', 'User Id', 'User Id', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.GuidFilterField},0 , 1, 1 ,0),";
            sql += $"('ApplicationUser', 'Email', 'email', 'email', 'User Email', 'User Email', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 1 ,0),";
            sql += $"('ApplicationUser', 'FirstName', 'firstName', 'firstName', 'User First Name', 'User First Name', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 1 ,0),";
            sql += $"('ApplicationUser', 'LastName', 'lastName', 'lastName', 'User Last Name', 'User Last Name', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 1 ,0),";
            sql += $"('ApplicationUser', 'PhoneNumber', 'phoneNumber', 'phoneNumber', 'User Phone Number', 'User Phone Number', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1 ,0),";
            sql += $"('ApplicationUser', 'Address', 'address', 'address', 'User Address', 'User Address', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1 ,0),";
            sql += $"('ApplicationUser', 'CountryId', 'country', 'country.id', 'User Country Id', 'User Country id', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1 ,1),";
            sql += $"('ApplicationUser', 'CountryName', 'country', 'country.countryName', 'User Country Name', 'User Country name', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1, 1),";
            sql += $"('ApplicationUser', 'DisplayLanguageId', 'displayLanguage', 'displayLanguage.id', 'User Display Language', 'User Display Language', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1 ,1),";
            sql += $"('ApplicationUser', 'DisplayLanguageName', 'displayLanguage', 'displayLanguage.id', 'User Display Language', 'User Display Language', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1, 1),";
            sql += $"('ApplicationUser', 'Roles', 'User Roles', 'roles', 'roles', 'User Roles', @categoryId, '[]',  '{metaDataJson}', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1 ,0);";
            
            migrationBuilder.Sql(sql);
            
            sql = $"SET @viewId = (SELECT Id FROM ConfigurableUserViews WHERE Name = 'Base View Of Users list');";
            sql += $"INSERT INTO ConfigurableUserViewHasConfigurableEntityColumnDefinitions (ConfigurableUserViewId, ConfigurableEntityColumnDefinitionId, Position) VALUES ";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ApplicationUser' and ColumnName = 'Id'), 1),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ApplicationUser' and ColumnName = 'Email'), 2),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ApplicationUser' and ColumnName = 'FirstName'), 3),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ApplicationUser' and ColumnName = 'LastName'), 4),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ApplicationUser' and ColumnName = 'PhoneNumber'), 5),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ApplicationUser' and ColumnName = 'Address'), 6),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ApplicationUser' and ColumnName = 'CountryId'), 7),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ApplicationUser' and ColumnName = 'CountryName'), 8),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ApplicationUser' and ColumnName = 'DisplayLanguageId'), 9),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ApplicationUser' and ColumnName = 'DisplayLanguageName'), 10),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ApplicationUser' and ColumnName = 'Roles'), 11);";
            
            migrationBuilder.Sql(sql);
            #endregion
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "ConfigurableCategories",
                "Name",
                "Baseic");
            
            string sql = $"SET @categoryId = (SELECT Id FROM ConfigurableCategories WHERE Name = 'Baseic');";
            sql += $"DELETE FROM ConfigurableEntityColumnDefinitions WHERE CategoryId = @categoryId;";
            sql += $"DELETE FROM ConfigurableUserViews WHERE Name = 'Base View Of Activity Logs';";
            sql += $"DELETE FROM ConfigurableUserViews WHERE Name = 'Base View Of Clients';";
            sql += $"DELETE FROM ConfigurableUserViews WHERE Name = 'Base View Of Client Contacts';";
            sql += $"DELETE FROM ConfigurableUserViews WHERE Name = 'Base View Of Users';";
            migrationBuilder.Sql(sql);
            
        }
    }
}
