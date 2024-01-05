using Infrastructure.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

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
                new[] { "Name", "Description", "Order"},
                new object[] { "Baseic", "Baseic", 1});
            
            #region ActivityLogs View
            
            string sql = $"SET @categoryId = (SELECT Id FROM ConfigurableCategories WHERE Name = 'Baseic');";
            sql += $"INSERT INTO ConfigurableEntityColumnDefinitions (EntityName, ColumnName, DisplayName, Description, CategoryId, Permissions, MetaData, DataType, FilterOperationType, IsSortable, IsFilterable) VALUES ";
            sql += $"('ActivityLogs', 'Id', 'Activity Log Id', 'Entity Id', @categoryId, '[]', '[]', {(int)EntityColumnDataType.Number}, {(int)EntityColumnOperationType.IdFilterField}, 1, 1),";
            sql += $"('ActivityLogs', 'OwnerUserId', 'Owner User Id', 'Owner User Id', @categoryId, '[\"Admin\",\"IT\"]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1),";
            sql += $"('ActivityLogs', 'UserId', 'User Id', 'User Id', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1),";
            sql += $"('ActivityLogs', 'EntityId', 'Entity Id', 'Entity Id', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1),";
            sql += $"('ActivityLogs', 'EntityType', 'Entity Type', 'Entity Type', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1),";
            sql += $"('ActivityLogs', 'OperationType', 'Operation Type', 'Operation Type', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1),";
            sql += $"('ActivityLogs', 'Activity', 'Activity', 'Activity', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 0, 1),";
            sql += $"('ActivityLogs', 'Details', 'Details', 'Details', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 0, 1),";
            sql += $"('ActivityLogs', 'Status', 'Status', 'Status', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1),";
            sql += $"('ActivityLogs', 'IpAddress', 'Ip Address', 'Ip Address', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1),";
            sql += $"('ActivityLogs', 'UserAgent', 'User Agent', 'User Agent', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 0, 1),";
            sql += $"('ActivityLogs', 'CreatedAt', 'Created At', 'Created At', @categoryId, '[]', '[]', {(int)EntityColumnDataType.Date}, {(int)EntityColumnOperationType.DateFilterField}, 1, 1);";
            migrationBuilder.Sql(sql);
            
            sql = $"SET @userId = (SELECT Id FROM AspNetUsers WHERE UserName='adminWolbergPro' limit 1);";
            sql += $"Insert into ConfigurableUserViews (UserId, Name, Description, IsShareAble, IsDefault, Color, Settings, IsPredefined) VALUES ";
            sql += $"(@userId, 'Base View Of Activity Logs', '', 1, 1, '#000000', '[]', 1);";
            migrationBuilder.Sql(sql);
            
            sql = $"SET @viewId = (SELECT Id FROM ConfigurableUserViews WHERE Name = 'Base View Of Activity Logs');";
            sql += $"INSERT INTO ConfigurableUserViewHasConfigurableEntityColumnDefinitions (ConfigurableUserViewId, ConfigurableEntityColumnDefinitionId, `Order`) VALUES ";
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
            sql += $"INSERT INTO ConfigurableEntityColumnDefinitions (EntityName, ColumnName, DisplayName, Description, CategoryId, Permissions, MetaData, DataType, FilterOperationType, IsHidden, IsSortable, IsFilterable) VALUES ";
            sql += $"('Clients', 'Id', 'Client Id', 'Entity Id', @categoryId, '[]', '[]', {(int)EntityColumnDataType.Number}, {(int)EntityColumnOperationType.IdFilterField},0 , 1, 1),";
            sql += $"('Clients', 'ParentId', 'Client Parent Id', 'Client Parent Id', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 0, 1),";
            sql += $"('Clients', 'OwnerUserId', 'Client Owner User', 'User Owner of the client', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 1),";
            sql += $"('Clients', 'Name', 'Client Name', 'Client Name', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 1),";
            sql += $"('Clients', 'Description', 'Client Description', 'Description', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 0, 1),";
            sql += $"('Clients', 'Website', 'Client Website', 'Client Website', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 0),";
            sql += $"('Clients', 'CountryId', 'Client Country Id', 'Client Country Id', @categoryId, '[]', '[]', {(int)EntityColumnDataType.Number}, {(int)EntityColumnOperationType.IdFilterField},0 , 1, 1),";
            sql += $"('Clients', 'Address', 'Client Address', 'Client Address', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 1),";
            sql += $"('Clients', 'City', 'Client City', 'Client City', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 1),";
            sql += $"('Clients', 'CreatedAt', 'Client Created At', 'Client Created At', @categoryId, '[]', '[]', {(int)EntityColumnDataType.Date}, {(int)EntityColumnOperationType.DateFilterField},0 , 1, 1);";
            
            migrationBuilder.Sql(sql);
            
            sql = $"SET @userId = (SELECT Id FROM AspNetUsers WHERE UserName='adminWolbergPro' limit 1);";
            sql += $"Insert into ConfigurableUserViews (UserId, Name, Description, IsShareAble, IsDefault, Color, Settings, IsPredefined) VALUES ";
            sql += $"(@userId, 'Base View Of Clients', '', 1, 1, '#000000', '[]', 1);";
            migrationBuilder.Sql(sql);
            
            sql = $"SET @viewId = (SELECT Id FROM ConfigurableUserViews WHERE Name = 'Base View Of Clients');";
            sql += $"INSERT INTO ConfigurableUserViewHasConfigurableEntityColumnDefinitions (ConfigurableUserViewId, ConfigurableEntityColumnDefinitionId, `Order`) VALUES ";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'Id'), 1),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'ParentId'), 2),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'OwnerUserId'), 3),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'Name'), 4),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'Description'), 5),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'Website'), 6),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'DisabledAt'), 7),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'Address'), 8),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'City'), 9),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'CreatedAt'), 10);";
            migrationBuilder.Sql(sql);
            
            #endregion
            
            #region ClientContacts View
            
            sql = $"SET @categoryId = (SELECT Id FROM ConfigurableCategories WHERE Name = 'Baseic');";
            sql += $"INSERT INTO ConfigurableEntityColumnDefinitions (EntityName, ColumnName, DisplayName, Description, CategoryId, Permissions, MetaData, DataType, FilterOperationType, IsHidden, IsSortable, IsFilterable) VALUES ";
            sql += $"('ClientContacts', 'Id', 'Client Contact Id', 'Entity Id', @categoryId, '[]', '[]', {(int)EntityColumnDataType.Number}, {(int)EntityColumnOperationType.IdFilterField},0 , 1, 1),";
            sql += $"('ClientContacts', 'ClientId', 'Client Id', 'Client Id', @categoryId, '[]', '[]', {(int)EntityColumnDataType.Number}, {(int)EntityColumnOperationType.IdFilterField},0 , 0, 0),";
            sql += $"('ClientContacts', 'FirstName', 'Client Contact First Name', 'Client Contact First Name', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 1),";
            sql += $"('ClientContacts', 'LastName', 'Client Contact Last Name', 'Client Contact Last Name', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 1),";
            sql += $"('ClientContacts', 'Email', 'Client Contact Email', 'Client Contact Email', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1),";
            sql += $"('ClientContacts', 'Phone1', 'Client Contact Phone 1', 'Client Contact Phone 1', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1),";
            sql += $"('ClientContacts', 'Phone2', 'Client Contact Phone 2', 'Client Contact Phone 2', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1),";
            sql += $"('ClientContacts', 'Fax', 'Client Contact Fax', 'Client Contact Fax', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 0, 0),";
            sql += $"('ClientContacts', 'Whatsapp', 'Client Contact Whatsapp', 'Client Contact Whatsapp', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 0, 0),";
            sql += $"('ClientContacts', 'CountryId', 'Client Contact Country Id', 'Client Contact Country Id', @categoryId, '[]', '[]', {(int)EntityColumnDataType.Number}, {(int)EntityColumnOperationType.IdFilterField},1 , 1, 1),";
            sql += $"('ClientContacts', 'PostalCode', 'Client Contact Postal Code', 'Client Contact Postal Code', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 0, 0),";
            sql += $"('ClientContacts', 'Address', 'Client Contact Address', 'Client Contact Address', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1),";
            sql += $"('ClientContacts', 'City', 'Client Contact City', 'Client Contact City', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1),";
            sql += $"('ClientContacts', 'State', 'Client Contact State', 'Client Contact State', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 0, 1),";
            sql += $"('ClientContacts', 'JobTitle', 'Client Contact Job Title', 'Client Contact Job Title', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 0, 1),";
            sql += $"('ClientContacts', 'Department', 'Client Contact Department', 'Client Contact Department', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 0, 1),";
            sql += $"('ClientContacts', 'Company', 'Client Contact Company', 'Client Contact Company', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1),";
            sql += $"('ClientContacts', 'Notes', 'Client Contact Notes', 'Client Contact Notes', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 0, 0),";
            sql += $"('ClientContacts', 'CreatedAt', 'Client Contact Created At', 'Client Contact Created At', @categoryId, '[]', '[]', {(int)EntityColumnDataType.Date}, {(int)EntityColumnOperationType.DateFilterField},1 , 1, 1);";
            
            migrationBuilder.Sql(sql);
            
            sql = $"SET @userId = (SELECT Id FROM AspNetUsers WHERE UserName='adminWolbergPro' limit 1);";
            sql += $"Insert into ConfigurableUserViews (UserId, Name, Description, IsShareAble, IsDefault, Color, Settings, IsPredefined) VALUES ";
            sql += $"(@userId, 'Base View Of Client Contacts', '', 1, 1, '#000000', '[]', 1);";
            migrationBuilder.Sql(sql);
            
            sql = $"SET @viewId = (SELECT Id FROM ConfigurableUserViews WHERE Name = 'Base View Of Client Contacts');";
            sql += $"INSERT INTO ConfigurableUserViewHasConfigurableEntityColumnDefinitions (ConfigurableUserViewId, ConfigurableEntityColumnDefinitionId, `Order`) VALUES ";
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
            
            #region User View
            
            sql = $"SET @categoryId = (SELECT Id FROM ConfigurableCategories WHERE Name = 'Baseic');";
            sql += $"INSERT INTO ConfigurableEntityColumnDefinitions (EntityName, ColumnName, DisplayName, Description, CategoryId, Permissions, MetaData, DataType, FilterOperationType, IsHidden, IsSortable, IsFilterable) VALUES ";
            sql += $"('ApplicationUser', 'Id', 'User Id', 'Entity Id', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.GuidFilterField},0 , 1, 1),";
            sql += $"('ApplicationUser', 'Email', 'User Email', 'User Email', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 1),";
            sql += $"('ApplicationUser', 'FirstName', 'User First Name', 'User First Name', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 1),";
            sql += $"('ApplicationUser', 'LastName', 'User Last Name', 'User Last Name', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 1),";
            sql += $"('ApplicationUser', 'PhoneNumber', 'User Phone Number', 'User Phone Number', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1),";
            sql += $"('ApplicationUser', 'Address', 'User Address', 'User Address', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1),";
            sql += $"('ApplicationUser', 'Country', 'User Country', 'User Country', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1),";
            sql += $"('ApplicationUser', 'DisplayLanguage', 'User Display Language', 'User Display Language', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1),";
            sql += $"('ApplicationUser', 'Roles', 'User Roles', 'User Roles', @categoryId, '[]', '[]', {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},1 , 1, 1);";
            
            migrationBuilder.Sql(sql);
            
            
            sql = $"SET @userId = (SELECT Id FROM AspNetUsers WHERE UserName='adminWolbergPro' limit 1);";
            sql += $"Insert into ConfigurableUserViews (UserId, Name, Description, IsShareAble, IsDefault, Color, Settings, IsPredefined) VALUES ";
            sql += $"(@userId, 'Base View Of Users', '', 1, 1, '#000000', '[]', 1);";

            migrationBuilder.Sql(sql);
            
            sql = $"SET @viewId = (SELECT Id FROM ConfigurableUserViews WHERE Name = 'Base View Of Users');";
            sql += $"INSERT INTO ConfigurableUserViewHasConfigurableEntityColumnDefinitions (ConfigurableUserViewId, ConfigurableEntityColumnDefinitionId, `Order`) VALUES ";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ApplicationUser' and ColumnName = 'Id'), 1),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ApplicationUser' and ColumnName = 'Email'), 2),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ApplicationUser' and ColumnName = 'FirstName'), 3),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ApplicationUser' and ColumnName = 'LastName'), 4),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ApplicationUser' and ColumnName = 'PhoneNumber'), 5),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ApplicationUser' and ColumnName = 'Address'), 6),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ApplicationUser' and ColumnName = 'Country'), 7),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ApplicationUser' and ColumnName = 'DisplayLanguage'), 8),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'ApplicationUser' and ColumnName = 'Roles'), 9);";
            
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
            sql += $"DELETE FROM ConfigurableUserViews WHERE Name = 'Base View Of Users';";
            migrationBuilder.Sql(sql);
            
        }
    }
}
