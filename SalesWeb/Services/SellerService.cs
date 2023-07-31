using SalesWeb.Data;
using SalesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWeb.Services.Exceptions;

namespace SalesWeb.Services {
    public class SellerService {

        private readonly SalesWebContext _context;

        public SellerService(SalesWebContext context) {
            _context = context;
        }

        public List<Seller> FindAll() {
            return _context.Seller.ToList(); // Por enquanto, SYNC.
        }

        public void Insert(Seller obj) {
            
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id) {
            return _context.Seller.Include(x => x.Department).FirstOrDefault(x => x.Id == id); // Eager loading
        }

        public void Remove(int id) {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj); // Removeu do Dbset, falta confirmar a deleção com o entity framework
            _context.SaveChanges(); // confirmei c o EF.
        }

        public void Update(Seller obj) {
            if (!_context.Seller.Any(x => x.Id == obj.Id)) {
                throw new NotFoundException("Id not found");
            }

            try {
                _context.Update(obj);
                _context.SaveChanges();
            } catch(DbUpdateConcurrencyException e) { // Se uma exceção de nivel de acesso a dados a minha camada de serviços vai lançar uma exceção da camada dela.
                throw new DbConcurrencyException(e.Message);
            }

        }

    }
}
