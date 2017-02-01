function calculaRFC() {
	function quitaArticulos(palabra) {
		return palabra.replace("DEL ", "").replace("LAS ", "").replace("DE ",
				"").replace("LA ", "").replace("Y ", "").replace("A ", "");
	}
	function esVocal(letra) {
		if (letra == 'A' || letra == 'E' || letra == 'I' || letra == 'O'
				|| letra == 'U' || letra == 'a' || letra == 'e' || letra == 'i'
				|| letra == 'o' || letra == 'u')
			return true;
		else
			return false;
	}

	nombre = $("#nombre").val().toUpperCase();

	apellidoPaterno = $("#apellidoPaterno").val().toUpperCase();

	apellidoMaterno = $("#apellidoMaterno").val().toUpperCase();

	fecha = $("#fechaNacimiento").val();

	if(nombre.length == 0)
	{
		alert("Campo Nombre vacio");
		return;
	}
	else if (apellidoPaterno.length==0) {
		alert("Campo Apellido paterno vacio");
		return;
	}
	else if (apellidoMaterno.length==0) {
		alert("Campo Apellido materno vacio");
		return;
	}
	else if (fecha.length==0) {
		alert("Campo Fecha de Nacimiento vacio");
		return;
	}

	var rfc = "";

	apellidoPaterno = quitaArticulos(apellidoPaterno);
	apellidoMaterno = quitaArticulos(apellidoMaterno);

	rfc += apellidoPaterno.substr(0, 1);

	var l = apellidoPaterno.length;
	var c;
	for (i = 0; i < l; i++) {
		c = apellidoPaterno.charAt(i);
		if (esVocal(c)) {
			rfc += c;
			break;
		}
	}


	rfc += apellidoMaterno.substr(0, 1);

	rfc += nombre.substr(0, 1);

	rfc += fecha.substr(2, 2);

	rfc += fecha.substr(5, 2).substr(0, 2);

	rfc += fecha.substr(8, 2);

	rfc += "XYZ";

	// ingresamos Valor a RFC

	$("#rfc").val(rfc);

}