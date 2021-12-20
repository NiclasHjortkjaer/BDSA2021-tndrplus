global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Identity.Web;
global using Microsoft.Identity.Web.Resource;
global using Microsoft.OpenApi.Models;
global using Azure.Storage.Blobs;

global using ProjectBank.Infrastructure;
global using ProjectBank.Infrastructure.Repository;
global using ProjectBank.Infrastructure.Entity;
global using ProjectBank.Server.Extensions;
global using ProjectBank.Core.RepositoryInterface;
global using ProjectBank.Core.Enum;
global using ProjectBank.Core.DTO;
global using static ProjectBank.Core.Enum.Status;