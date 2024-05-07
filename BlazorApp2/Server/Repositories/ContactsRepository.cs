using BlazorApp2.Shared.Models;

namespace BlazorApp2.Server.Repositories
{
    public class ContactsRepository : GenericRepository<Contact>
    {
        private List<Contact> records;

        public ContactsRepository()
        {
            records = new List<Contact>();
        }

        public Contact? Add(Contact entity)
        {
            if (IsExist(entity.Email))
                return null;

            if (records.Count == 0)
                entity.ID = 1;
            else
                entity.ID = records.OrderByDescending(r => r.ID).FirstOrDefault().ID + 1;
            records.Add(entity);
            return entity;
        }

        public Contact Get(int id)
        {
            return records.Find(c => c.ID == id);
        }

        public IEnumerable<Contact> GetAll()
        {
            return records;
        }

        public bool Remove(int id)
        {
            var existingContact = Get(id);
            records.Remove(existingContact);
            return true;
        }

        public Contact? Update(int id, Contact entity)
        {
            var existingContact = Get(id);
/*            if (existingContact == null)
                return null;
*/
            existingContact.Email = entity.Email;
            existingContact.FirstName = entity.FirstName;
            existingContact.LastName = entity.LastName;
            existingContact.PhoneNumber = entity.PhoneNumber;

            return existingContact;
        }

        public bool IsExist(string email)
        {
            return records != null && records.Any(x => x.Email == email);
        }
        public bool IsExist(int id)
        {
            return records != null && records.Any(x => x.ID == id);
        }
    }
}
