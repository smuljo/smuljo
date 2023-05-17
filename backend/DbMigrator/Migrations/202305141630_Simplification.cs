namespace DbMigrator.Migrations;

[TimestampedMigration(2023, 5, 14, 16, 30)]
public sealed class Simplification : ForwardOnlyMigration
{
    public override void Up()
    {
        Delete
            .Column("AvatarKey").FromTable("User");

        Delete
            .Column("PhotoKey").FromTable("Topic");

        Delete
            .Column("MaterialType").FromTable("Material");
    }
}