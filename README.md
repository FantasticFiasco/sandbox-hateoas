<div align="center">
  <img src="doc/resources/logo.png">
</div>

# HATEOAS sandbox

This repository is a sandbox for different implementations of [HATEOAS](https://en.wikipedia.org/wiki/HATEOAS). Currently only one implementation exist but more will possibly be added in the future.

## Domain model

Imagine there to be a site where you as an author can post articles. You can also read articles from other authors, and collaborate by posting comments to those articles.

The following graph describe the relations between authors, articles and comments.

![Entity relations UML](https://g.gravizo.com/source/custom_relations_uml?https%3A%2F%2Fraw.githubusercontent.com%2FFantasticFiasco%2Fsandbox-hateoas%2Fdocs%2Freadme%2Fdoc%2FUML.md)

## Implementations

These are the implementations that currently exist in this repository:

- [.NET Core backend with the HAL-browser as client](./src/hal/dotnet-and-hal-browser)