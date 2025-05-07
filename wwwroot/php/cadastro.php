<?php
header("Access-Control-Allow-Origin: *"); // para testes locais
header("Content-Type: application/json");

// Conexão com o banco
$conn = new mysqli("localhost", "root", "", "sabor_brasil");

if ($conn->connect_error) {
    die(json_encode(["success" => false, "message" => "Erro na conexão com o banco."]));
}

// Pega os dados enviados via POST
$data = json_decode(file_get_contents("php://input"), true);

$nome = $conn->real_escape_string($data["nome"]);
$email = $conn->real_escape_string($data["email"]);
$nickname = $conn->real_escape_string($data["nickname"]);
$senha = password_hash($data["senha"], PASSWORD_DEFAULT); // senha criptografada

// Insere no banco
$sql = "INSERT INTO usuarios (nome, email, nickname, senha) VALUES ('$nome', '$email', '$nickname', '$senha')";

if ($conn->query($sql) === TRUE) {
    echo json_encode(["success" => true, "message" => "Usuário cadastrado com sucesso."]);
} else {
    echo json_encode(["success" => false, "message" => "Erro ao cadastrar: " . $conn->error]);
}

$conn->close();
?>