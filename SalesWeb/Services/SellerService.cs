using SalesWeb.Data;
using SalesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWeb.Services.Exceptions;
using System.Threading.Tasks;

namespace SalesWeb.Services {
    public class SellerService {

        private readonly SalesWebContext _context;

        public SellerService(SalesWebContext context) {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync() {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller obj) {

            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id) {
            return await _context.Seller.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id); // Eager loading
        }

        public async Task RemoveAsync(int id) {
            try {
                var obj = _context.Seller.Find(id);
                _context.Seller.Remove(obj); // Removeu do Dbset, falta confirmar a deleção com o entity framework
                await _context.SaveChangesAsync(); // confirmei c o EF.
            } catch (DbUpdateException e) {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Seller obj) {

            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny) {
                throw new NotFoundException("Id not found");
            }

            try {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e) { // Se uma exceção de nivel de acesso a dados a minha camada de serviços vai lançar uma exceção da camada dela.
                throw new DbConcurrencyException(e.Message);
            }

        }

    }
}
