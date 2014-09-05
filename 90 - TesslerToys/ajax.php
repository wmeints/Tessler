<?php include('shared/_header.php'); ?>

<h1>Ajax</h1>
<p>When using ajax, the Selenium driver will sometimes be too early pressing buttons or reading texts.
Tessler compensates for this by asking the javascript framework whether any ajax requests are pending and waiting for them to complete.</p>

<script type="text/javascript">
$(function	() {
	$('#fast-ajax-button').click(function () {
		$.getJSON('api/ajax.php?fast', function (data) {
			$('#fast-ajax-text').html(data.text);
		});
	});
});
</script>
<h4>Fast ajax</h4>
<p id="fast-ajax-text">When you click the button under this text, a new text will be loaded using ajax, instantly.</p>
<button id="fast-ajax-button" class="btn btn-default">Do fast ajax call</button>

<script type="text/javascript">
$(function	() {
	$('#slow-ajax-button').click(function () {
		$.getJSON('api/ajax.php?slow', function (data) {
			$('#slow-ajax-text').html(data.text);
		});
	});
});
</script>
<h4>Slow ajax</h4>
<p id="slow-ajax-text">This ajax call will take some time, like a few seconds.</p>
<button id="slow-ajax-button" class="btn btn-default">Do slow ajax call</button>

<script type="text/javascript">
$(function	() {
	$('#very-slow-ajax-button').click(function () {
		$.getJSON('api/ajax.php?very-slow', function (data) {
			$('#very-slow-ajax-text').html(data.text);
		});
	});
});
</script>
<h4>Very slow ajax</h4>
<p id="very-slow-ajax-text">When this button is pressed, a really heavy backend operation will be emulated, around 5 seconds.</p>
<button id="very-slow-ajax-button" class="btn btn-default">Do very slow ajax call</button>

<?php include('shared/_footer.php'); ?>