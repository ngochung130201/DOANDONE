using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;
using Polly;
using Share.Data;
using Share.Exceptions;
using Share.Hepers.Upload;
using Share.ListException.Brand;
using Share.Logger;
using Share.Models;
using Share.Repository;
using Share.UnitOfWork;
using Share.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Contact = Share.Models.Contact;

namespace Share.Services.ContactService
{
    public class ContactService : IContactService
    {

        private readonly DoAnBanHangContext _context;
        private readonly IUnitOfWork<DoAnBanHangContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IRepositoryBase<Contact> _repositoryBase;
        public ContactService(IUnitOfWork<DoAnBanHangContext> unitOfWork, RepositoryBase<Contact> repositoryBase,
             IMapper mapper,
             DoAnBanHangContext context,
             ILoggerManager logger
            )
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _repositoryBase = repositoryBase;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Contact> AddEnity(ContactVM enity)
        {



            var mapperContact = _mapper.Map<Contact>(enity);
            var reuslt = await _repositoryBase.AddEnity(mapperContact);
            _unitOfWork.SaveChanges();
            return reuslt;





        }

        public async Task<bool> DeleteEnity(Contact enity)
        {
            await _repositoryBase.DeleteEnity(enity);
            _unitOfWork.SaveChanges();
            throw new OKRequestException($"Xóa {enity.Name} thành công");
        }



        public async Task<List<Contact>> GetAllAsync()
        {
            return await _repositoryBase.GetAllAsync();



        }

        public async Task<Contact> GetByWhereAsync(int id)
        {
            var contact = await _repositoryBase.GetByWhereAsync(id);
            if (contact == null)
            {
                {
                    throw new NotFoundException("Không tìm thấy liên hệ");
                }
            }
            return contact;

        }



        public void SaveChangeAsync()
        {
            _unitOfWork.SaveChanges();
        }

        public async Task<bool> UpdateEnity(int Id, ContactVM enity)
        {
            var getcontact = await _repositoryBase.GetByWhereAsync(Id);
            if (getcontact == null)
            {
                throw new NotFoundException($"Không tìm thấy liên hệ {enity.Name}");
            }
            getcontact.Subject = enity.Subject;
            getcontact.Body = enity.Body;
            getcontact.Phone = enity.Phone;
            getcontact.Email = enity.Email;
            getcontact.CreateDate = DateTime.Now;
            getcontact.Address = enity.Address;
            await _repositoryBase.UpdateEnity(Id, getcontact);
            _unitOfWork.SaveChanges();

            throw new BrandOKException($"Cập nhật liên hệ thành công");
        }
        public Task<bool> DeleteEnitys(int[] Ids)
        {
            try
            {
                foreach (var item in Ids)
                {
                    var contact = _context.Contacts.Where(x => x.ContactId == item).FirstOrDefault();
                    if (contact == null)
                    {
                        throw new NotFoundException($"Không tìm thấy liên hệ {contact.Name}");
                    }
                    _context.Contacts.Remove(contact);
                    _context.SaveChanges();
                }
                throw new OKRequestException($"Xóa liên hệ thành công");
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"Xóa liên hệ thất bại {ex.Message}");
            }
        }

    }
}