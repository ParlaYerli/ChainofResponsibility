using System;

namespace ChainofResponsibility.Example2
{
    public abstract class OrderHandler
    {
        protected OrderHandler nextHandler;

        public void SetNextHandler(OrderHandler nextHandler)
        {
            this.nextHandler = nextHandler;
        }

        public void Handle(Order order)
        {
            try
            {
                if (ProcessOrder(order))
                {
                    Console.WriteLine("İşlem başarılı, zincir devam ediyor.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("İşlem başarısız, zincir tamamlandı.");
                Console.WriteLine($"Hata: {ex.Message}");
                return;
            }

            if (nextHandler != null)
            {
                nextHandler.Handle(order);
            }
        }

        protected abstract bool ProcessOrder(Order order);
    }

    // Concrete Handler 1: Order 
    public class ValidationHandler : OrderHandler
    {
        protected override bool ProcessOrder(Order order)
        {
            if (order.IsValid)
            {
                Console.WriteLine("Sipariş geçerli.");
                return true;
            }
            else
            {
                throw new Exception("Sipariş geçersiz.");
            }
        }
    }

    // Concrete Handler 2: Stock Check
    public class StockHandler : OrderHandler
    {
        protected override bool ProcessOrder(Order order)
        {
            if (order.IsInStock)
            {
                Console.WriteLine("Stokta yeterli ürün var.");
                return true;
            }
            else
            {
                throw new Exception("Stokta yeterli ürün yok.");
            }
        }
    }

    // Concrete Handler 3: Payment 
    public class PaymentHandler : OrderHandler
    {
        protected override bool ProcessOrder(Order order)
        {
            if (order.IsPaymentCompleted)
            {
                Console.WriteLine("Ödeme başarıyla tamamlandı.");
                return true; 
            }
            else
            {
                throw new Exception("Ödeme başarısız.");
            }
        }
    }


    // Concrete Handler 4: Invoice Generation
    public class InvoiceHandler : OrderHandler
    {
        protected override bool ProcessOrder(Order order)
        {
            Console.WriteLine("Fatura oluşturuldu ve müşteriye gönderildi.");
            return true; 
        }
    }

    // Order class
    public class Order
    {
        public bool IsValid { get; set; }
        public bool IsPaymentCompleted { get; set; }
        public bool IsInStock { set; get; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order
            {
                IsValid = true,              // Sipariş geçerli 
                IsInStock = false,            // Stokta ürün var
                IsPaymentCompleted = true   // Ödeme başarısız
            };

            var validation = new ValidationHandler();
            var payment = new PaymentHandler();
            var stock = new StockHandler();
            var invoice = new InvoiceHandler();

            validation.SetNextHandler(stock);
            stock.SetNextHandler(payment);
            payment.SetNextHandler(invoice);

            validation.Handle(order);
        }
    }

}
