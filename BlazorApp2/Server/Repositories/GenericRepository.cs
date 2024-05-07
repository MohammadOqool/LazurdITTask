using BlazorApp2.Server.Interfaces;
using BlazorApp2.Shared.Models;
using System.Linq.Expressions;

namespace BlazorApp2.Server.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
    {

        /* private List<T> records;

         public GenericRepository()
         {
             records = new List<T>();
         }

         public T? Add(T entity)
         {
             records.Add(entity);
             return entity;
         }

         public T Get(string email)
         {
             return records.FirstOrDefault(c => c.Email == email);
         }



         public IEnumerable<T> GetAll()
         {
             return records;
         }

         public bool Remove(string email)
         {
             var existingContact = Get();
             records.Remove(existingContact);
             return true;
         }

         public T? Update(T entity)
         {
             var existingContact = Get(entity.Email);
             if (existingContact == null)
                 return default;

             existingContact.Update();

             records.Email = entity.Email;
             existingContact.FirstName = entity.FirstName;
             existingContact.LastName = entity.LastName;
             existingContact.PhoneNumber = entity.PhoneNumber;

             return existingContact;
         }*/
        public T? Add(T entity)
        {
            throw new NotImplementedException();
        }

        public T Get(string email)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public T? Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
