using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TicketPlatform.Core.DataAccess;
using TicketPlatform.Core;

namespace TicketPlatform.Core.Migrations
{
    [DbContext(typeof(TicketContext))]
    [Migration("20170313093800_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TicketPlatform.Core.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int>("Role");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("TicketPlatform.Core.Performance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Performances");
                });

            modelBuilder.Entity("TicketPlatform.Core.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BuyerId");

                    b.Property<int?>("PerformanceId");

                    b.Property<decimal>("Price");

                    b.Property<int?>("SellerId");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("PerformanceId");

                    b.HasIndex("SellerId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("TicketPlatform.Core.Ticket", b =>
                {
                    b.HasOne("TicketPlatform.Core.Customer", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerId");

                    b.HasOne("TicketPlatform.Core.Performance", "Performance")
                        .WithMany()
                        .HasForeignKey("PerformanceId");

                    b.HasOne("TicketPlatform.Core.Customer", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId");
                });
        }
    }
}
