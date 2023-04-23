namespace DbMigrator.Migrations;

[TimestampedMigration(2023, 4, 23, 15, 36)]
public sealed class InitialMigration : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("User")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("UserName").AsString().Unique().NotNullable()
            .WithColumn("PasswordHash").AsString().NotNullable()
            .WithColumn("AvatarKey").AsString().Nullable();

        Create.Table("Topic")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("PhotoKey").AsString().Nullable()
            .WithColumn("MainTopicId").AsInt32().ForeignKey("Topic", "Id").Nullable();

        Create.Table("Comment")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("TopicId").AsInt32().ForeignKey("Topic", "Id").NotNullable()
            .WithColumn("UserId").AsInt32().ForeignKey("User", "Id").NotNullable()
            .WithColumn("Text").AsString().NotNullable();

        Create.Table("Material")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("CommentId").AsInt32().ForeignKey("Comment", "Id").NotNullable()
            .WithColumn("MaterialType").AsString().NotNullable()
            .WithColumn("Link").AsString().NotNullable();
    }
}