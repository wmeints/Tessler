<?php

$nav = array(
	'Home' => 'index.php',
	'jQuery' => 'jquery.php',
	'Tables' => 'tables.php',
	'Ajax' => 'ajax.php',
	'Popups' => 'popups.php',
	'Forms' => 'forms.php',
	'Special cases' => 'special_cases.php',
	'White' => 'white.php'
)

?>
<!doctype html>
<html>
	<head>
		<title>Tessler Toys - Automating Fun Since 1989!</title>
		<link type="text/css" rel="stylesheet" href="css/bootstrap.min.css" />
		<link type="text/css" rel="stylesheet" href="css/bootstrap-theme.min.css" />
		<link type="text/css" rel="stylesheet" href="css/site.css" />
		<?php if (!isset($noJQuery)) { ?>
		<script src="//ajax.googleapis.com/ajax/libs/jquery/2.0.3/jquery.min.js"></script>
		<?php } ?>
		<script src="//cdnjs.cloudflare.com/ajax/libs/knockout/3.0.0/knockout-min.js"></script>
		<script src="js/bootstrap.min.js"></script>
		<script>
			$(function () {
			$('#update-text').click(function () {
			$('h3').text('Automating Fun Since 1989!');
			});
			});
		</script>
	</head>
	<body>
		<div class="navbar navbar-inverse navbar-fixed-top" role="navigation">
			<div class="container">
				<div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
						<span class="sr-only">Toggle navigation</span>
						<span class="icon-bar"></span>
						<span class="icon-bar"></span>
						<span class="icon-bar"></span>
					</button>
					<a class="navbar-brand" href="#">Tessler Toys</a>
				</div>
				<div class="collapse navbar-collapse">
					<ul class="nav navbar-nav">
						<?php foreach($nav as $title => $link): ?>
						<li <?php if (strpos($_SERVER['REQUEST_URI'], $link) !== FALSE) { echo 'class="active"'; } ?>>
							<a href="<?php echo $link; ?>"><?php echo $title; ?></a>
						</li>
						<?php endforeach; ?>
					</ul>
				</div><!--/.nav-collapse -->
			</div>
		</div>
		<div class="container">
		<!--
		- Simpele selectors
		- Uitgebreide jQuery spullen
		- laden van jQuery
		- wachten op ajax
		- wachten op langzame page loads
		- meerdere elementen
		- elementen die niet bestaan
		- knockout animation
		- popups
		- visibility
		-->