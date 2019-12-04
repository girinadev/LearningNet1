using System;

namespace TaskApp.TaskClasses
{
  public class User
  {
    private readonly string _firstName;

    private readonly string _lastName;

    private readonly string _login;

    private readonly DateTime _createDate;

    public User(string firstName, string lastName, string login, DateTime createDate)
    {
      this._firstName = firstName;
      this._lastName = lastName;
      this._login = login;
      this._createDate = createDate;
    }

    public string FirstName => _firstName;
    public string LastName => _lastName;
    public string Login => _login;
    public DateTime CreateDate => _createDate;
  }
}
