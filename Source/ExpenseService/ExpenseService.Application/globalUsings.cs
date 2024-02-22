global using System;

global using System.Collections.Generic;
global using System.Linq;
global using System.Threading.Tasks;

global using AutoMapper;
global using MediatR;
global using MassTransit;


global using ExpenseService.Application.Common;
global using ExpenseService.Application.Shared;
global using ExpenseService.Domain.Repositories;
global using ExpenseService.Domain.Shared.Common;

global using ExpenseService.Infrastructure.Repositories;
global using ExpenseService.Infrastructure.Data;

global using ExpenseService.Domain.Events;

global using ExpenseService.Domain.Specifications.CategorySpecifications;
global using ExpenseService.Domain.Specifications.CategorySpecifications.Composite;

global using Serilog;
global using FluentValidation;

global using ExpenseService.Application.Interfaces;
global using ExpenseService.Application.Services;