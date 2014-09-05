<?php include('shared/_header.php'); ?>

<h1>Popups</h1>
<p>Popups should be represented by a so-called 'child object', a seperate class which only contains methods for the particular popup.</p>

<h4>Simple popup</h4>
<button class="btn btn-primary" data-toggle="modal" data-target="#simple-modal">Open simple popup</button>

<div class="modal fade" id="simple-modal">
	<div class="modal-dialog">
		<div class="modal-content">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
				<h4 class="modal-title">Simple modal dialog</h4>
			</div>
			<div class="modal-body">
				<p>This is a sample dialog, to test child objects.</p>
			</div>
			<div class="modal-footer">
				<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
			</div>
		</div>
	</div>
</div>

<h4>Popup with multiple buttons</h4>
<button class="btn btn-primary" data-toggle="modal" data-target="#button-modal">Open button popup</button>

<div class="modal fade" id="button-modal">
	<div class="modal-dialog">
		<div class="modal-content">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
				<h4 class="modal-title">This popup contains buttons</h4>
			</div>
			<div class="modal-body">
				<p>Which button is it gonna be?</p>
			</div>
			<div class="modal-footer">
				<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
				<button type="button" class="btn btn-primary" data-dismiss="modal">Save changes</button>
			</div>
		</div>
	</div>
</div>

<h4>Popup containing a table</h4>
<button class="btn btn-primary" data-toggle="modal" data-target="#table-modal">Open table popup</button>

<div class="modal fade" id="table-modal">
	<div class="modal-dialog">
		<div class="modal-content">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
				<h4 class="modal-title">Popup containing a table</h4>
			</div>
			<div class="modal-body">
				<p>This popup contains a table.</p>
				
				<table id="products" class="table">
				  <tr>
					<th style="width: 40px;">Id</th>
					<th>Name</th>
					<th style="width: 100px;">Price</th>
					<th style="width: 100px;">In stock</th>
				  </tr>
				  <tr>
					<td>1</td>
					<td>Space scooter</td>
					<td>40,00</td>
					<td>14</td>
				  </tr>
				  <tr>
					<td>2</td>
					<td>Lego technic truck</td>
					<td>55,00</td>
					<td>23</td>
				  </tr>
				  <tr>
					<td>3</td>
					<td>Cars puzzel</td>
					<td>10,99</td>
					<td>12</td>
				  </tr>
				  <tr>
					<td>4</td>
					<td>Pokemon X 3DS</td>
					<td>49,99</td>
					<td>17</td>
				  </tr>
				</table>
			</div>
			<div class="modal-footer">
				<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
			</div>
		</div>
	</div>
</div>

<?php include('shared/_footer.php'); ?>