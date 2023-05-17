namespace DbMigrator.Migrations;

[TimestampedMigration(2023, 5, 14, 16, 45)]
public sealed class AddTitleToTopic : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Column("Title").OnTable("Topic").AsString().NotNullable().Unique();
    }
}