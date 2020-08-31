
import * as Yup from 'yup';

export const SignInShema = Yup.object().shape({
  email: Yup.string().email('Invalid email').required('Required'),
  password: Yup.string()
    .min(6, 'Too Short!')
    .max(15, 'Too Long!')
    .required('Required'),
});

export const SignUpSchema = Yup.object().shape({
  firstName: Yup.string()
    .min(2, 'Too Short!')
    .max(50, 'Too Long!')
    .required('Required'),
  lastName: Yup.string()
    .min(2, 'Too Short!')
    .max(50, 'Too Long!')
    .required('Required'),
  email: Yup.string().email('Invalid email').required('Required'),
  password: Yup.string()
    .min(6, 'Too Short!')
    .max(15, 'Too Long!')
    .required('Required'),
  confirmPassword: Yup.string()
    .required('Required')
    .oneOf([Yup.ref('password'), ], 'Passwords must match'),
});

export const ForgotPasswordSchema = Yup.object().shape({
  email: Yup.string().email('Invalid email').required('Required'),
});

export const ChangePasswordSchema = Yup.object().shape({
  newPassword: Yup.string()
    .required()
    .min(6, 'Too Short!')
    .max(16, 'We prefer insecure system, try a shorter password.'),
  confirmPassword: Yup.string()
    .required()
    .test('passwords-match', 'Passwords must be matched', function (value) {
      return this.parent.newPassword === value;
    }),
});



