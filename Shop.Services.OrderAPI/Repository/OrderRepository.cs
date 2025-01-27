using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Services.OrderAPI.DbContexts;
using Shop.Services.OrderAPI.Models;
using Shop.Services.OrderAPI.Models.DTO;
using Shop.Services.OrderAPI.Repositoty;

namespace Shop.Services.OrderAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public OrderRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<bool> DeleteOrder(int id)
        {
            try
            {
                //OrderDetails orderDetails = await _db.OrderDetails.FirstOrDefaultAsync(u => u.OrderDetailsId == id);
                //if (orderDetails == null)
                //{
                //    return false;
                //}
                //_db.OrderDetails.Remove(orderDetails);
                //await _db.SaveChangesAsync();
                return true;

            }
            catch
            {
                return false;
            }
        }

        public async Task<OrderDTO> GetOrderByUserId(string id)
        {
            Order order = new()
            {
                orderDetails = await _db.OrderDetails.FirstOrDefaultAsync(u => u.UserId == id)

            };
            order.cartDetails = _db.CartDetails
                .Where(u => u.OrderDetailsId == order.orderDetails.OrderDetailsId).Include(u => u.Product);
            return _mapper.Map<OrderDTO>(order);

        }

        public async Task<IEnumerable<OrderDTO>> GetOrders()
        {
            //            var coursesContext = await _context.Courses
            //.Join(_context.Teachers,
            //  courses => courses.IdTeacher,
            //  teacher => teacher.IdTeacher,
            //  (courses, teacher) => new CourseViewModel
            //  {
            //      IdCourse = courses.IdCourse,
            //      NameCourse = courses.NameCourse,
            //      DiscriptionCourse = courses.DiscriptionCourse,
            //      DateStartCourse = courses.DateStartCourse,
            //      DateEndCourse = courses.DateEndCourse,
            //      Surname = teacher.Surname,
            //      Name = teacher.Name
            //  })
            //.ToListAsync();




            var orders = new List<Order>();

            // Получение всех OrderDetails с CartDetails и Product
            var cartDetails = _db.CartDetails
                .Include(od => od.OrderDetails)
                .Include(cd => cd.Product)
                .ToList();

            // Группировка OrderDetails по OrderDetailsId и создание списка Order
            var groupedOrderDetails = cartDetails.GroupBy(od => od.OrderDetailsId);
            foreach (var group in groupedOrderDetails)
            {
                var order = new Order
                {
                    cartDetails =  group,
                    orderDetails = group.FirstOrDefault().OrderDetails // Берем первый элемент из группы OrderDetails
                
                };
                
                orders.Add(order);
            }

            return _mapper.Map<List<OrderDTO>>(orders);
           
        }

        public  OrderDetailsDTO CreateUpdateStatusOrders(OrderDetailsDTO orderDetailsDTO)
        {

            //OrderDetails orderDetails = _mapper.Map<OrderDetails>(orderDetailsDTO);
            ////check if product exists in database, if not create it
            //var prodInDb = await _db.Products.FirstOrDefaultAsync(u =>
            //u.ProductId == orderDetailsDTO.CartDetails.FirstOrDefault().ProductId);
            //if (prodInDb == null)
            //{
            //    _db.Products.Add(cart.CartDetails.FirstOrDefault().Product);
            //    await _db.SaveChangesAsync();
            //}
            ////check if header is null
            //var cartHeaderFromDb = await _db.CartHeader.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == cart.CartHeader.UserId);
            //if (cartHeaderFromDb == null)
            //{
            //    //create header and details
            //    //_db.CartHeader.Add(cart.CartHeader);
            //    //await _db.SaveChangesAsync();
            //    cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;
            //    cart.CartDetails.FirstOrDefault().Product = null;
            //    _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
            //    await _db.SaveChangesAsync();
            //}
            //else
            //{
            //    //if header is not null
            //    //check if details has same product
            //    var cartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(
            //        u => u.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
            //        u.CartHeaderId == cartHeaderFromDb.CartHeaderId);
            //    if (cartDetailsFromDb == null)
            //    {
            //        //create details
            //        cart.CartDetails.FirstOrDefault().CartDetailsId = 0;
            //        cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeaderFromDb.CartHeaderId;
            //        cart.CartDetails.FirstOrDefault().CartHeader = null;
            //        cart.CartDetails.FirstOrDefault().Product = null;
            //        _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
            //        await _db.SaveChangesAsync();
            //    }
            //    else
            //    {
            //        // update the count
            //        //cart.CartDetails.FirstOrDefault().Product = null;
            //        //cart.CartDetails.FirstOrDefault().Count += cartDetailsFromDb.Count;
            //        cartDetailsFromDb.Count += cart.CartDetails.FirstOrDefault().Count;
            //        _db.CartDetails.Update(cartDetailsFromDb);
            //        await _db.SaveChangesAsync();

            //    }
            //}
            return _mapper.Map<OrderDetailsDTO>(new OrderDetails());

            //return _mapper.Map<CartDTO>(cart);

        }

        public async Task<OrderDTO> CreateUpdateStatusOrders(OrderDTO orderDTO)
        {
			Order order = _mapper.Map<Order>(orderDTO);
			//check if product exists in database, if not create it
			var prodInDb = await _db.Products.FirstOrDefaultAsync(u =>
			u.ProductId == orderDTO.cartDetails.FirstOrDefault().ProductId);
			if (prodInDb == null)
			{
			    _db.Products.Add(order.cartDetails.FirstOrDefault().Product);
			    await _db.SaveChangesAsync();
			}
            //check if header is null
            var orderHeaderFromDb = new OrderDetails();

			//create header and details
			//_db.CartHeader.Add(cart.CartHeader);
			//await _db.SaveChangesAsync();
			order.cartDetails.FirstOrDefault().OrderDetailsId = order.orderDetails.OrderDetailsId;
			order.cartDetails.FirstOrDefault().Product = null;
			    //_db.CartDetails.Add(order.cartDetails.FirstOrDefault());
			    //await _db.SaveChangesAsync();

			//if header is not null
			//check if details has same product
			//var cartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(
			//    u => u.ProductId == order.cartDetails.FirstOrDefault().ProductId &&
			//    u.OrderDetailsId == orderHeaderFromDb.CartHeaderId);
			//if (cartDetailsFromDb == null)
			//{
			//create details
			order.cartDetails.FirstOrDefault().CartDetailsId = 0;
			order.cartDetails.FirstOrDefault().OrderDetailsId = order.orderDetails.OrderDetailsId;
			order.cartDetails.FirstOrDefault().OrderDetails = null;
			order.cartDetails.FirstOrDefault().Product = null;
			        _db.CartDetails.Add(order.cartDetails.FirstOrDefault());
			        await _db.SaveChangesAsync();
			//}
			//else
			//{
			//    // update the count
			//    //cart.CartDetails.FirstOrDefault().Product = null;
			//    //cart.CartDetails.FirstOrDefault().Count += cartDetailsFromDb.Count;
			//    cartDetailsFromDb.Count += cart.CartDetails.FirstOrDefault().Count;
			//    _db.CartDetails.Update(cartDetailsFromDb);
			//    await _db.SaveChangesAsync();

			//}
			return _mapper.Map<OrderDTO>(order);
		}
	}
}
