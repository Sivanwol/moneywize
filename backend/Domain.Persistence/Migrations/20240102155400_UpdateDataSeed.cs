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
            
            string sql = $"DECLARE @categoryId INT; SET @categoryId = (SELECT Id FROM ConfigurableCategories WHERE Name = 'Baseic');";
            sql += $"INSERT INTO ConfigurableEntityColumnDefinitions (EntityName, ColumnName, DisplayName, Description, CategoryId, DataType, FilterOperationType, IsSortable, IsFilterable) VALUES ";
            sql += $"('ActivityLogs', 'Id', 'Activity Log Id', 'Entity Id', @categoryId, {(int)EntityColumnDataType.Number}, {(int)EntityColumnOperationType.IdFilterField}, 1, 1),";
            sql += $"('ActivityLogs', 'OwnerUserId', 'Owner User Id', 'Owner User Id', @categoryId, {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1),";
            sql += $"('ActivityLogs', 'UserId', 'User Id', 'User Id', @categoryId, {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1),";
            sql += $"('ActivityLogs', 'EntityId', 'Entity Id', 'Entity Id', @categoryId, {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1),";
            sql += $"('ActivityLogs', 'EntityType', 'Entity Type', 'Entity Type', @categoryId, {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1),";
            sql += $"('ActivityLogs', 'OperationType', 'Operation Type', 'Operation Type', @categoryId, {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1),";
            sql += $"('ActivityLogs', 'Activity', 'Activity', 'Activity', @categoryId, {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 0, 1),";
            sql += $"('ActivityLogs', 'Details', 'Details', 'Details', @categoryId, {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 0, 1),";
            sql += $"('ActivityLogs', 'Status', 'Status', 'Status', @categoryId, {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1),";
            sql += $"('ActivityLogs', 'IpAddress', 'Ip Address', 'Ip Address', @categoryId, {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 1, 1),";
            sql += $"('ActivityLogs', 'UserAgent', 'User Agent', 'User Agent', @categoryId, {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField}, 0, 1),";
            sql += $"('ActivityLogs', 'CreatedAt', 'Created At', 'Created At', @categoryId, {(int)EntityColumnDataType.Date}, {(int)EntityColumnOperationType.DateFilterField}, 1, 1);";
            migrationBuilder.Sql(sql);
            sql = $"DECLARE @userId VARCHAR(36); SET @userId = (SELECT Id FROM AspNetUsers WHERE Id = 'ef75787a-a95f-48f6-8b18-3698459ca244' limit 1);";
            sql += $"Insert into ConfigurableUserViews (UserId, Name, Description, IsShareAble, IsDefault, Color, Settings, IsPredefined) VALUES ";
            sql += $"(@userId, 'Base View Of Activity Logs', '', 1, 1, '#000000', '[]', 1);";
            migrationBuilder.Sql(sql);
            
            sql = $"DECLARE @viewId INT; SET @viewId = (SELECT Id FROM ConfigurableUserViews WHERE Name = 'Base View Of Activity Logs');";
            sql += $"INSERT INTO ConfigurableUserViewHasConfigurableEntityColumnDefinitions (ConfigurableUserViewId, ConfigurableEntityColumnDefinitionId) VALUES ";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE ColumnName = 'Id')),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE ColumnName = 'OwnerUserId')),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE ColumnName = 'UserId')),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE ColumnName = 'EntityId')),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE ColumnName = 'EntityType')),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE ColumnName = 'OperationType')),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE ColumnName = 'Activity')),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE ColumnName = 'Details')),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE ColumnName = 'Status')),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE ColumnName = 'IpAddress')),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE ColumnName = 'UserAgent')),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE ColumnName = 'CreatedAt'));";
            migrationBuilder.Sql(sql);
            
            #endregion
            
            #region Client View
            
            sql = $"DECLARE @categoryId INT; SET @categoryId = (SELECT Id FROM ConfigurableCategories WHERE Name = 'Baseic');";
            sql += $"INSERT INTO ConfigurableEntityColumnDefinitions (EntityName, ColumnName, DisplayName, Description, CategoryId, DataType, FilterOperationType, IsHidden, IsSortable, IsFilterable) VALUES ";
            sql += $"('Clients', 'Id', 'Client Id', 'Entity Id', @categoryId, {(int)EntityColumnDataType.Number}, {(int)EntityColumnOperationType.IdFilterField},0 , 1, 1),";
            sql += $"('Clients', 'ParentId', 'Client Parent Id', 'Client Parent Id', @categoryId, {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 0),";
            sql += $"('Clients', 'OwnerUserId', 'Client Owner User', 'User Owner of the client', @categoryId, {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 1),";
            sql += $"('Clients', 'Name', 'Client Name', 'Client Name', @categoryId, {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 1),";
            sql += $"('Clients', 'Description', 'Client Description', 'Description', @categoryId, {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 0, 1),";
            sql += $"('Clients', 'Website', 'Client Website', 'Client Website', @categoryId, {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 0),";
            sql += $"('Clients', 'DisabledAt', 'Client Disabled At', 'Client Disabled At', @categoryId, {(int)EntityColumnDataType.Date}, {(int)EntityColumnOperationType.DateFilterField},1 , 0, 0),";
            sql += $"('Clients', 'Address', 'Client Address', 'Client Address', @categoryId, {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 1);";
            sql += $"('Clients', 'City', 'Client City', 'Client City', @categoryId, {(int)EntityColumnDataType.String}, {(int)EntityColumnOperationType.StringFilterField},0 , 1, 1);";
            sql += $"('Clients', 'CreatedAt', 'Client Created At', 'Client Created At', @categoryId, {(int)EntityColumnDataType.Date}, {(int)EntityColumnOperationType.DateFilterField},0 , 1, 1);";
            
            migrationBuilder.Sql(sql);
            
            sql = $"DECLARE @viewId INT; SET @viewId = (SELECT Id FROM ConfigurableUserViews WHERE Name = 'Base View Of Activity Logs');";
            sql += $"Insert into ConfigurableUserViews (UserId, Name, Description, IsShareAble, IsDefault, Color, Settings, IsPredefined) VALUES ";
            sql += $"(@viewId, 'Base View Of Clients', '', 1, 1, '#000000', '[]', 1);";
            migrationBuilder.Sql(sql);
            
            sql = $"DECLARE @viewId INT; SET @viewId = (SELECT Id FROM ConfigurableUserViews WHERE Name = 'Base View Of Clients');";
            sql += $"INSERT INTO ConfigurableUserViewHasConfigurableEntityColumnDefinitions (ConfigurableUserViewId, ConfigurableEntityColumnDefinitionId) VALUES ";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'Id')),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'ParentId')),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'OwnerUserId')),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'Name')),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'Description')),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'Website')),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'DisabledAt')),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'Address')),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'City')),";
            sql += $"(@viewId, (SELECT Id FROM ConfigurableEntityColumnDefinitions WHERE EntityName = 'Clients' and ColumnName = 'CreatedAt'));";
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
            
            string sql = $"DECLARE @categoryId INT; SET @categoryId = (SELECT Id FROM ConfigurableCategories WHERE Name = 'Baseic');";
            sql += $"DELETE FROM ConfigurableEntityColumnDefinitions WHERE CategoryId = @categoryId;";
            sql += $"DELETE FROM ConfigurableUserViews WHERE Name = 'Base View Of Activity Logs';";
            sql += $"DELETE FROM ConfigurableUserViews WHERE Name = 'Base View Of Clients';";
            migrationBuilder.Sql(sql);
            
        }
    }
}
