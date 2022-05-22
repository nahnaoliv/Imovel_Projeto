--
-- Estrutura da tabela `imovel`
--

CREATE TABLE `imovel` (
  `Id` int(11) NOT NULL,
  `Nome` text DEFAULT NULL,
  `Descricao` text DEFAULT NULL,
  `Preco` decimal(18,2) NOT NULL,
  `Cep` text DEFAULT NULL,
  `Rua` text DEFAULT NULL,
  `Numero` int(11) NOT NULL,
  `Bairro` text DEFAULT NULL,
  `Complemento` text DEFAULT NULL,
  `Cidade` text DEFAULT NULL,
  `Estado` text DEFAULT NULL,
  `Referencia` text DEFAULT NULL,
  `Alugado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Índices para tabela `imovel`
--
ALTER TABLE `imovel`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT de tabela `imovel`
--
ALTER TABLE `imovel`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;
