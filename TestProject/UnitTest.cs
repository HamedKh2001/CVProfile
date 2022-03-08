using CoreLayer.IServices;
using CoreLayer.Services;
using CVProfile.Areas.Admin.Controllers;
using CVProfile.Controllers;
using DataLayer.Context;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TestProject
{
	public class UnitTest
	{
		[Fact]
		public void Getall()
		{
			//Arrange
			//var booklist = new List<Skill>
			//{
			//	new Skill()
			//	{
			//		CreationDate = DateTime.Now,
			//		Id = 100,
			//		Name ="skill1",
			//		PercentOfDominance=50,
			//	},new Skill()
			//	{
			//		CreationDate = DateTime.Now.AddDays(5),
			//		Id = 200,
			//		Name ="skill2",
			//		PercentOfDominance=66,
			//	},
			//}.AsQueryable();
			////var bookMock = new Mock<DbSet<Skill>>();

			////var mockContext = new Mock<CVContext>();
			////mockContext.Setup(m => m.Skills).Returns(bookMock.Object);
			//var productRepositoryMock = new Mock<IGenericRepository<Skill>>();
			//productRepositoryMock.Setup(m => m.GetAll()).Returns(booklist).Verifiable();


			////bookMock.As<IQueryable<Skill>>().Setup(m => m.Provider).Returns(booklist.Provider).Verifiable();
			////bookMock.As<IQueryable<Skill>>().Setup(m => m.Expression).Returns(booklist.Expression).Verifiable();
			////bookMock.As<IQueryable<Skill>>().Setup(m => m.ElementType).Returns(booklist.ElementType).Verifiable();
			////bookMock.As<IQueryable<Skill>>().Setup(m => m.GetEnumerator()).Returns(booklist.GetEnumerator()).Verifiable();

			////var mockContext = new Mock<CVContext>();
			////mockContext.Setup(m => m.Skills).Returns(bookMock.Object);

			////Act
			////var repository = new GenericRepository<Skill>(mockContext.Object);
			////var actual = repository.GetAll().ToList();
			//var controller = new SkillController(productRepositoryMock.Object);
			//var actual = controller.Index() as ViewResult;
			////Assert
			//Assert.NotNull(actual.Model);
			//Assert.Equal("skill1", actual.FirstOrDefault().Name);
		}
	}
}
