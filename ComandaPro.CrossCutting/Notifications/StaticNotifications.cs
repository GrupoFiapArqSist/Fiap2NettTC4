namespace ComandaPro.CrossCutting.Notifications;

public static class StaticNotifications
{
    #region [Users]
    public static Notification InvalidCredentials = new("InvalidCredentials", "Credenciais invalidas!");
    public static Notification UserAlreadyExists = new("UserAlreadyExists", "Usuario já cadastrado!");
    public static Notification UserNotFound = new("InvalidUser", "Usuario não encontrado!");
    public static Notification RevokeToken = new("RevokeToken", "Token revogado com sucesso!");
    public static Notification InvalidToken = new("InvalidToken", "Token invalido!");
    public static Notification UserCreated = new ("UserCreated", "Usuario criado com sucesso!");
    public static Notification UsernameAlreadyExists = new("UsernameAlreadyExists", "Username já está sendo utilizado!");
    public static Notification UserEdited = new("UserEdited", "Usuario editado com sucesso!");
    public static Notification PasswordChanged = new("PasswordChanged", "Senha alterada com sucesso!");
    public static Notification PhotoUploaded = new("PhotoUploaded", "Upload da foto realizado com sucesso!");
    public static Notification UserDeleted = new("UserDeleted", "Usuario removido com sucesso!");
    public static Notification UserActivated = new("UserActivated", "Ativação de usuário alterada com sucesso!");
    public static Notification UserApproved = new("UserApproved", "Usuario aprovado com sucesso!");
    #endregion

    #region [Order]
    public static Notification OrderSuccess = new("OrderSuccess", "Pedido e itens inserido com sucesso!");
    public static Notification OrderError = new("OrderError", "Pedido com erro.");
    public static Notification OrderNotFound = new("OrderNotFound", "Não há pedidos nesta comanda.");
    public static Notification OrderDeleteSucess = new("OrderDeleteSucess", "Pedido excluído com sucesso.");
    public static Notification OrderDeleteCommandSucess = new("OrderDeleteCommandSucess", "Pedidos excluídos com sucesso e valores recalculados.");
    public static Notification OrderItemsUpdateSucess = new("OrderItemsUpdateSucess", "Pedido atualizado com sucesso e valores recalculados.");

    #endregion

    #region [Product]
    public static Notification ProductSucess = new("ProductSucess", "Produto inserido com sucesso!");
    public static Notification ProductError = new("ProductError", "Produto com erro.");
    public static Notification ProductDeleteSucess = new("ProductDeleteSucess", "Produto excluido com sucesso!");
    public static Notification ProductUpdateSucess = new("ProductUpdateSucess", "Produto atualizado com sucesso!");
    public static Notification ProductNotExists = new("ProductNotExists", "Produto não existe.");
    public static Notification ProductAlreadyExists = new("ProductAlreadyExists", "Produto já existente.");
    #endregion

    #region [Category]
    public static Notification CategorySucess = new("CategorySucess", "Categoria inserida com sucesso!");
    public static Notification CategoryError = new("CategoryError", "Categoria com erro.");
    public static Notification CategoryDeleteSucess = new("CategoryDeleteSucess", "Categoria excluida com sucesso!");
    public static Notification CategoryNotExists = new("CategoryNotExists", "Categoria não existe.");
    public static Notification CategoryAlreadyExists = new("CategoryAlreadyExists", "Categoria já existente.");
    public static Notification CategoryConflict = new("CategoryConflict", "Categoria está atribuída a um produto ativo no cardápio.");
    #endregion

	#region [Command]
	public static Notification CommandError = new("CommandError", "Comanda com erro.");
	public static Notification CommandIsAlreadyOpened = new("CommandIsAlreadyOpened", "Comanda já está aberta.");
	public static Notification CommandOpenedSuccess = new("CommandOpenedSuccess", "Comanda aberta com sucesso.");
    public static Notification CommandNotFound = new("CommandNotFound", "Comanda não encontrada.");
    public static Notification CommandIsAlreadyClosed = new("CommandIsAlreadyClosed", "Comanda já está fechada.");
    public static Notification CommandClosedSuccess = new("CommandClosedSuccess", "Comanda fechada com sucesso.");
	#endregion
}
