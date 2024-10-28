Funcionalidade: Pagamento com QR Code

Cenário: Geração de QR code para pagamento de novo pedido
	Dado um pedido com id 8af14f25-d209-4b2b-b5ab-458f0b5913b1 
	E com valor de 50.0
	E com tipo de pagamento QRCode	
	Quando a solicitação de pagamento para o pedido for enviada
	Então deve retornar um QR Code para pagamento do pedido
	E deve retornar o id do pagamento

Cenário: Geração de QR code para pagamento que já foi pago
	Dado um pedido com id 9ea6a3c0-5abf-447b-810f-beca4ba7011b 
	E que consta como pago
	Quando a solicitação de pagamento para o pedido for enviada
	Então deve falhar a operação para o pedido