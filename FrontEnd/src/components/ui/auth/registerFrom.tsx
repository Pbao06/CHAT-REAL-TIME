import { useState } from "react";
import { Eye, EyeOff, Loader2 } from "lucide-react";

import { Button } from "@/components/ui/button";
import { Checkbox } from "@/components/ui/checkbox";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";

export interface RegisterFormProps {
  fullName: string;
  email: string;
  password: string;
  confirmPassword: string;
  agreeTerms: boolean;
  loading?: boolean;
  errors?: {
    fullName?: string;
    email?: string;
    password?: string;
    confirmPassword?: string;
    agreeTerms?: string;
    general?: string;
  };
  onFullNameChange: (value: string) => void;
  onEmailChange: (value: string) => void;
  onPasswordChange: (value: string) => void;
  onConfirmPasswordChange: (value: string) => void;
  onAgreeTermsChange: (checked: boolean) => void;
  onSubmit: (e: React.FormEvent<HTMLFormElement>) => void;
}

export function RegisterForm({
  fullName,
  email,
  password,
  confirmPassword,
  agreeTerms,
  loading = false,
  errors,
  onFullNameChange,
  onEmailChange,
  onPasswordChange,
  onConfirmPasswordChange,
  onAgreeTermsChange,
  onSubmit,
}: RegisterFormProps) {
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);

  return (
    <form onSubmit={onSubmit} className="space-y-5" noValidate>
      {errors?.general ? (
        <p
          role="alert"
          className="rounded-lg border border-destructive/30 bg-destructive/10 px-3 py-2 text-sm text-destructive"
        >
          {errors.general}
        </p>
      ) : null}

      <div className="space-y-2">
        <Label htmlFor="full-name">Full name</Label>
        <Input
          id="full-name"
          type="text"
          autoComplete="name"
          placeholder="Jane Doe"
          value={fullName}
          disabled={loading}
          aria-invalid={Boolean(errors?.fullName)}
          aria-describedby={errors?.fullName ? "full-name-error" : undefined}
          onChange={(event) => onFullNameChange(event.target.value)}
        />
        {errors?.fullName ? (
          <p id="full-name-error" className="text-sm text-destructive">
            {errors.fullName}
          </p>
        ) : null}
      </div>

      <div className="space-y-2">
        <Label htmlFor="register-email">Email</Label>
        <Input
          id="register-email"
          type="email"
          autoComplete="email"
          placeholder="you@example.com"
          value={email}
          disabled={loading}
          aria-invalid={Boolean(errors?.email)}
          aria-describedby={errors?.email ? "register-email-error" : undefined}
          onChange={(event) => onEmailChange(event.target.value)}
        />
        {errors?.email ? (
          <p id="register-email-error" className="text-sm text-destructive">
            {errors.email}
          </p>
        ) : null}
      </div>

      <div className="space-y-2">
        <Label htmlFor="register-password">Password</Label>
        <div className="relative">
          <Input
            id="register-password"
            type={showPassword ? "text" : "password"}
            autoComplete="new-password"
            placeholder="••••••••"
            value={password}
            disabled={loading}
            aria-invalid={Boolean(errors?.password)}
            aria-describedby={errors?.password ? "register-password-error" : undefined}
            onChange={(event) => onPasswordChange(event.target.value)}
            className="pr-10"
          />
          <Button
            type="button"
            variant="ghost"
            size="icon"
            disabled={loading}
            aria-label={showPassword ? "Hide password" : "Show password"}
            aria-pressed={showPassword}
            onClick={() => setShowPassword((visible) => !visible)}
            className="absolute inset-y-0 right-0 my-auto size-9 text-muted-foreground hover:bg-transparent hover:text-foreground"
          >
            {showPassword ? (
              <EyeOff className="size-4" aria-hidden="true" />
            ) : (
              <Eye className="size-4" aria-hidden="true" />
            )}
          </Button>
        </div>
        {errors?.password ? (
          <p id="register-password-error" className="text-sm text-destructive">
            {errors.password}
          </p>
        ) : null}
      </div>

      <div className="space-y-2">
        <Label htmlFor="confirm-password">Confirm password</Label>
        <div className="relative">
          <Input
            id="confirm-password"
            type={showConfirmPassword ? "text" : "password"}
            autoComplete="new-password"
            placeholder="••••••••"
            value={confirmPassword}
            disabled={loading}
            aria-invalid={Boolean(errors?.confirmPassword)}
            aria-describedby={errors?.confirmPassword ? "confirm-password-error" : undefined}
            onChange={(event) => onConfirmPasswordChange(event.target.value)}
            className="pr-10"
          />
          <Button
            type="button"
            variant="ghost"
            size="icon"
            disabled={loading}
            aria-label={showConfirmPassword ? "Hide password" : "Show password"}
            aria-pressed={showConfirmPassword}
            onClick={() => setShowConfirmPassword((visible) => !visible)}
            className="absolute inset-y-0 right-0 my-auto size-9 text-muted-foreground hover:bg-transparent hover:text-foreground"
          >
            {showConfirmPassword ? (
              <EyeOff className="size-4" aria-hidden="true" />
            ) : (
              <Eye className="size-4" aria-hidden="true" />
            )}
          </Button>
        </div>
        {errors?.confirmPassword ? (
          <p id="confirm-password-error" className="text-sm text-destructive">
            {errors.confirmPassword}
          </p>
        ) : null}
      </div>

      <div className="space-y-2">
        <div className="flex items-start gap-2">
          <Checkbox
            id="agree-terms"
            checked={agreeTerms}
            disabled={loading}
            aria-invalid={Boolean(errors?.agreeTerms)}
            aria-describedby={errors?.agreeTerms ? "agree-terms-error" : undefined}
            onCheckedChange={(checked) => onAgreeTermsChange(checked === true)}
            className="mt-0.5"
          />
          <Label htmlFor="agree-terms" className="text-sm font-normal text-muted-foreground">
            I agree to the{" "}
            <a href="#" className="font-medium text-foreground underline-offset-4 hover:underline">
              Terms
            </a>{" "}
            &amp;{" "}
            <a href="#" className="font-medium text-foreground underline-offset-4 hover:underline">
              Privacy Policy
            </a>
          </Label>
        </div>
        {errors?.agreeTerms ? (
          <p id="agree-terms-error" className="text-sm text-destructive">
            {errors.agreeTerms}
          </p>
        ) : null}
      </div>

      <Button type="submit" className="w-full rounded-lg" disabled={loading}>
        {loading ? (
          <>
            <Loader2 className="size-4 animate-spin" aria-hidden="true" />
            Creating account...
          </>
        ) : (
          "Create account"
        )}
      </Button>

      <p className="text-center text-sm text-muted-foreground">
        Already have an account?{" "}
        <a href="#" className="font-medium text-foreground underline-offset-4 hover:underline">
          Sign in
        </a>
      </p>
    </form>
  );
}

export default RegisterForm;