﻿using BlazorShop.Api.Mappings;
using BlazorShop.Api.Repositories;
using BlazorShop.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetItens()
        {
            try
            {
                var produtos = await _produtoRepository.GetItens();
                if(produtos is null)
                {
                    return NotFound();
                }
                else
                {
                    var produtosDtos = produtos.ConverterProdutosParaDto();
                        return Ok(produtosDtos);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao acessar a base de dados");

            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProdutoDto>> GetItem(int id)
        {
            try
            {
                var produto = await _produtoRepository.GetItem(id);
                if (produto is null)
                {
                    return NotFound("Produto não localizado");
                }
                else
                {
                    var produtosDtos = produto.ConverterProdutoParaDto();
                    return Ok(produtosDtos);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao acessar a base de dados");

            }
        }
        [HttpGet]
        [Route("GetItensPorCategoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>>GetItensPorCategoria(int categoriaId)
        {
            try
            {
                var produtos = await _produtoRepository.GetItensPorCategoria(categoriaId);
                var produtosDto = produtos.ConverterProdutosParaDto();
                return Ok(produtosDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao acessar o banco de dados");
            }
        }

    }
}
