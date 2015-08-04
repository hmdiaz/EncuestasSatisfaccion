<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WEB.Pages.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Acceso al Sistema - Encuesta de Satisfacción Kioscos Vive Digital</title>
    <!-- stylesheets are put here, refer to files/css documentation -->
    <link href="../Client/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Client/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Client/css/ace-fonts.css" rel="stylesheet" type="text/css" />
    <link href="../Client/css/ace.min.css" rel="stylesheet" type="text/css" />
    <!-- some scripts should be  here, refer to files/javascript documentation -->
    <script src="../Client/js/ace-extra.min.js" type="text/javascript"></script>
</head>
<body class="login-layout light-login">
    <form runat="server">
	    <div class="main-container">
		    <div class="main-content">
			    <div class="row">
				    <div class="col-sm-10 col-sm-offset-1">
					    <div class="login-container">
						    <div class="center">
							    <h1>
								    <!--<i class="ace-icon fa fa-leaf green"></i>
                                    <span class="red">Encuestas de Satisfacción KVD</span>
                                    -->
                                    <img src="../Images/logo_azteca_gde.png" />								
							    </h1>
							    <h4 class="blue" id="id-company-text">&copy; Azteca Comunicaciones Colombia</h4>
						    </div>

						    <div class="space-6"></div>

						    <div class="position-relative">
                                <asp:ValidationSummary HeaderText="<center><h4>Los siguientes campos son requeridos</h4></center>" CssClass="alert alert-danger" ID="ValidationSummary1" runat="server" ValidationGroup="1" />
                                <asp:Panel ID="PanelResultado" runat="server" CssClass="alert alert-danger">
                                    <button type="button" class="close" data-dismiss="alert">
				                        <i class="ace-icon fa fa-times"></i>
			                        </button>
                                    <asp:Label ID="Label1" runat="server" Text="El usuario no se encuentra registrado, por favor verifique e intente nuevamente."></asp:Label>
                                </asp:Panel>
							    <div id="login-box" class="login-box visible widget-box no-border">
								    <div class="widget-body">
									    <div class="widget-main">
										    <h4 class="header blue lighter bigger">
											    <i class="ace-icon fa fa-coffee green"></i>
											    Por favor digite el Email
										    </h4>

										    <div class="space-6"></div>

										    <form>
											    <fieldset>
												    <label class="block clearfix">
													    <span class="block input-icon input-icon-right">
                                                            <asp:TextBox CssClass="form-control" ID="txtCorreoElectronico" runat="server" placeholder="Correo Electrónico..."></asp:TextBox>
														    <i class="ace-icon fa fa-user"></i>
                                                            <asp:RegularExpressionValidator 
                                                                            ValidationGroup="1" 
                                                                            ID="RegularExpressionValidator1" 
                                                                            runat="server" 
                                                                            ControlToValidate="txtCorreoElectronico"
                                                                            ErrorMessage="(*) El Formato del Correo es Incorrecto. Ejemplo: correo@azteca-comunicaciones.com" 
                                                                            Text="<span style='color:red; display:inline'></span>"
                                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                            </asp:RegularExpressionValidator>
                                                            <asp:RequiredFieldValidator 
                                                                            ValidationGroup="1" 
                                                                            ID="RequiredFieldValidator1" 
                                                                            runat="server" 
                                                                            Text="<span style='color:red'></span>"
                                                                            ErrorMessage="(*) El Correo Electrónico es requerido."
                                                                            ControlToValidate="txtCorreoElectronico">                                                
                                                            </asp:RequiredFieldValidator>
													    </span>
												    </label>

												    <div class="space"></div>

												    <div class="clearfix">
                                                        <asp:Button ID="Button1" runat="server" 
                                                            CssClass="width-35 pull-right btn btn-sm btn-primary" 
                                                            ValidationGroup="1" 
                                                            Text="Ingresar" 
                                                            onclick="Button1_Click" />													
												    </div>

												    <div class="space-4"></div>
											    </fieldset>
										    </form>

									    </div><!-- /.widget-main -->

									
								    </div><!-- /.widget-body -->
							    </div><!-- /.login-box -->
                                
							    <div id="forgot-box" class="forgot-box widget-box no-border">
								    <div class="widget-body">
									    <div class="widget-main">
										    <h4 class="header red lighter bigger">
											    <i class="ace-icon fa fa-key"></i>
											    Retrieve Password
										    </h4>

										    <div class="space-6"></div>
										    <p>
											    Enter your email and to receive instructions
										    </p>

										    <form>
											    <fieldset>
												    <label class="block clearfix">
													    <span class="block input-icon input-icon-right">
														    <input type="email" class="form-control" placeholder="Email" />
														    <i class="ace-icon fa fa-envelope"></i>
													    </span>
												    </label>

												    <div class="clearfix">
													    <button type="button" class="width-35 pull-right btn btn-sm btn-danger">
														    <i class="ace-icon fa fa-lightbulb-o"></i>
														    <span class="bigger-110">Send Me!</span>
													    </button>
												    </div>
											    </fieldset>
										    </form>
									    </div><!-- /.widget-main -->

									    <div class="toolbar center">
										    <a href="#" data-target="#login-box" class="back-to-login-link">
											    Back to login
											    <i class="ace-icon fa fa-arrow-right"></i>
										    </a>
									    </div>
								    </div><!-- /.widget-body -->
							    </div><!-- /.forgot-box -->

							    <div id="signup-box" class="signup-box widget-box no-border">
								    <div class="widget-body">
									    <div class="widget-main">
										    <h4 class="header green lighter bigger">
											    <i class="ace-icon fa fa-users blue"></i>
											    New User Registration
										    </h4>

										    <div class="space-6"></div>
										    <p> Enter your details to begin: </p>

										    <form>
											    <fieldset>
												    <label class="block clearfix">
													    <span class="block input-icon input-icon-right">
														    <input type="email" class="form-control" placeholder="Email" />
														    <i class="ace-icon fa fa-envelope"></i>
													    </span>
												    </label>

												    <label class="block clearfix">
													    <span class="block input-icon input-icon-right">
														    <input type="text" class="form-control" placeholder="Username" />
														    <i class="ace-icon fa fa-user"></i>
													    </span>
												    </label>

												    <label class="block clearfix">
													    <span class="block input-icon input-icon-right">
														    <input type="password" class="form-control" placeholder="Password" />
														    <i class="ace-icon fa fa-lock"></i>
													    </span>
												    </label>

												    <label class="block clearfix">
													    <span class="block input-icon input-icon-right">
														    <input type="password" class="form-control" placeholder="Repeat password" />
														    <i class="ace-icon fa fa-retweet"></i>
													    </span>
												    </label>

												    <label class="block">
													    <input type="checkbox" class="ace" />
													    <span class="lbl">
														    I accept the
														    <a href="#">User Agreement</a>
													    </span>
												    </label>

												    <div class="space-24"></div>

												    <div class="clearfix">
													    <button type="reset" class="width-30 pull-left btn btn-sm">
														    <i class="ace-icon fa fa-refresh"></i>
														    <span class="bigger-110">Reset</span>
													    </button>

													    <button type="button" class="width-65 pull-right btn btn-sm btn-success">
														    <span class="bigger-110">Register</span>

														    <i class="ace-icon fa fa-arrow-right icon-on-right"></i>
													    </button>
												    </div>
											    </fieldset>
										    </form>
									    </div>

									    <div class="toolbar center">                                        
										    <a href="#" data-target="#login-box" class="back-to-login-link">
											    <i class="ace-icon fa fa-arrow-left"></i>
											    Back to login
										    </a>
									    </div>
								    </div><!-- /.widget-body -->
							    </div><!-- /.signup-box -->
						    </div><!-- /.position-relative -->
					    </div>
				    </div><!-- /.col -->
			    </div><!-- /.row -->
		    </div><!-- /.main-content -->
	    </div><!-- /.main-container -->
    </form>
	<!-- basic scripts -->

	<!--[if !IE]> -->
	<script type="text/javascript">
		window.jQuery || document.write("<script src='../assets/js/jquery.min.js'>" + "<" + "/script>");
	</script>

	<!-- <![endif]-->

	<!--[if IE]>
<script type="text/javascript">
window.jQuery || document.write("<script src='../assets/js/jquery1x.min.js'>"+"<"+"/script>");
</script>
<![endif]-->
	<script type="text/javascript">
		if ('ontouchstart' in document.documentElement) document.write("<script src='../assets/js/jquery.mobile.custom.min.js'>" + "<" + "/script>");
	</script>

	<!-- inline scripts related to this page -->
	<script type="text/javascript">
		jQuery(function ($) {
		    $(document).on('click', '.toolbar a[data-target]', function (e) {
		        e.preventDefault();
		        var target = $(this).data('target');
		        $('.widget-box.visible').removeClass('visible'); //hide others
		        $(target).addClass('visible'); //show target
		    });
		});



		//you don't need this, just used for changing background
		jQuery(function ($) {
		    $('#btn-login-dark').on('click', function (e) {
		        $('body').attr('class', 'login-layout');
		        $('#id-text2').attr('class', 'white');
		        $('#id-company-text').attr('class', 'blue');

		        e.preventDefault();
		    });
		    $('#btn-login-light').on('click', function (e) {
		        $('body').attr('class', 'login-layout light-login');
		        $('#id-text2').attr('class', 'grey');
		        $('#id-company-text').attr('class', 'blue');

		        e.preventDefault();
		    });
		    $('#btn-login-blur').on('click', function (e) {
		        $('body').attr('class', 'login-layout blur-login');
		        $('#id-text2').attr('class', 'white');
		        $('#id-company-text').attr('class', 'light-blue');

		        e.preventDefault();
		    });

		});
	</script>
</body>
</html>
