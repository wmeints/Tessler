<?php

if (isset($_GET['fast'])) {
	echo json_encode(array(
		'text' => 'Hurray! It worked!'
	));
}

if (isset($_GET['slow'])) {
	sleep(2);
	
	echo json_encode(array(
		'text' => 'Took some time, but it worked again!'
	));
}

if (isset($_GET['very-slow'])) {
	sleep(5);
	
	echo json_encode(array(
		'text' => 'Woah, that was just plain slow!'
	));
}

?>