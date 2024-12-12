using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PZ_18.Models;

namespace PZ_18.Tests
{
	[TestFixture]
	public class ModelTests
	{
		[Test]
		public void ChangePassword_UpdatesPasswordCorrectly()
		{
			var user = new User
			{
				FIO = "Иванов Иван Иванович",
				Phone = "89999999999",
				Login = "ivanov",
				Password = "oldpass",
				TypeID = 1
			};

			user.ChangePassword("newHashedPass");

			// Было: Assert.AreEqual("newHashedPass", user.Password, ...)
			Assert.That(user.Password, Is.EqualTo("newHashedPass"), "Пароль должен обновиться на newHashedPass");
		}

		[Test]
		public void UpdateStatus_IfStatusIsReadyToGiveOut_SetCompletionDate()
		{
			var request = new Request
			{
				StartDate = DateTime.Now.AddDays(-2),
				TechTypeID = 1,
				HomeTechModel = "ModelX",
				ProblemDescription = "Не работает",
				RequestStatus = "В процессе ремонта",
				ClientFIO = "Петров Петр Петрович",
				ClientPhone = "89991234567"
			};

			request.UpdateStatus("Готова к выдаче");

			// Было: Assert.AreEqual("Готова к выдаче", request.RequestStatus, ...)
			Assert.That(request.RequestStatus, Is.EqualTo("Готова к выдаче"), "Статус должен обновиться на 'Готова к выдаче'");

			// Было: Assert.IsNotNull(request.CompletionDate, ...)
			Assert.That(request.CompletionDate, Is.Not.Null, "CompletionDate должен быть установлен");
		}

		[Test]
		public void Request_Validation_ShouldFailIfMissingRequiredFields()
		{
			var request = new Request
			{
				StartDate = DateTime.Now,
				TechTypeID = 1,
				HomeTechModel = "ModelX",
				ProblemDescription = "Не работает"
			};

			var validationResults = new List<ValidationResult>();
			var context = new ValidationContext(request, serviceProvider: null, items: null);

			bool isValid = Validator.TryValidateObject(request, context, validationResults, true);

			// Было: Assert.IsFalse(isValid)
			Assert.That(isValid, Is.False, "Объект не должен быть валиден без обязательных полей");

			// Было: Assert.IsTrue(validationResults.Count > 0)
			Assert.That(validationResults.Count, Is.GreaterThan(0), "Должны быть ошибки валидации");

			var invalidMembers = new List<string>();
			foreach (var vr in validationResults)
			{
				invalidMembers.AddRange(vr.MemberNames);
			}

			// Было: CollectionAssert.Contains(invalidMembers, "RequestStatus")
			Assert.That(invalidMembers, Contains.Item("RequestStatus"));
			Assert.That(invalidMembers, Contains.Item("ClientFIO"));
			Assert.That(invalidMembers, Contains.Item("ClientPhone"));
		}

		[Test]
		public void Request_Validation_ShouldPassIfAllRequiredFieldsAreSet()
		{
			var request = new Request
			{
				StartDate = DateTime.Now,
				TechTypeID = 1,
				HomeTechModel = "ModelX",
				ProblemDescription = "Не работает",
				RequestStatus = "Новая заявка",
				ClientFIO = "Иванов Иван Иванович",
				ClientPhone = "89999999999"
			};

			var validationResults = new List<ValidationResult>();
			var context = new ValidationContext(request, serviceProvider: null, items: null);

			bool isValid = Validator.TryValidateObject(request, context, validationResults, true);

			// Было: Assert.IsTrue(isValid)
			Assert.That(isValid, Is.True, "Объект должен быть валиден, так как все поля есть");

			// Было: Assert.IsTrue(validationResults.Count == 0)
			Assert.That(validationResults.Count, Is.EqualTo(0), "Не должно быть ошибок валидации");
		}
	}
}
