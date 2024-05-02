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
    public static Notification OrderDeleteSucess = new("OrderDeleteSucess", "Pedido excluido com sucesso.");
    public static Notification OrderDeleteCommandSucess = new("OrderDeleteCommandSucess", "Pedidos excluidos com sucesso e valores recalculados.");
    public static Notification OrderItemsUpdateSucess = new("OrderItemsUpdateSucess", "Pedido atualizado com sucesso e valores recalculados.");
    #endregion
}
