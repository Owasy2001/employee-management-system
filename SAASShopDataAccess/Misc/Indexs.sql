DROP INDEX IF EXISTS IX_PHShops_ShopNo ON dbo.AppUserRoles
DROP INDEX IF EXISTS IX_Owners_OwnerNo ON dbo.Owners
GO

CREATE INDEX IX_PHShops_ShopNo ON dbo.AppUserRoles (ShopNo);
GO
CREATE INDEX IX_Owners_OwnerNo ON dbo.Owners (OwnerNo);
GO
