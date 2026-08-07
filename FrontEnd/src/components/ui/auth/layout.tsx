import type { PropsWithChildren } from "react";
import { MessageCircle } from "lucide-react";

import { Card, CardContent } from "@/components/ui/card";

const APP_NAME = "Chat Real Time";
const APP_SUBTITLE = "Sign in to continue";

export function AuthLayout({ children }: PropsWithChildren) {
  return (
    <div className="flex min-h-svh w-full items-center justify-center bg-muted/40 px-4 py-10 sm:px-6">
      <div className="w-full max-w-sm sm:max-w-md">
        <header className="mb-6 flex flex-col items-center gap-3 text-center">
          <span className="flex size-12 items-center justify-center rounded-2xl bg-primary text-primary-foreground shadow-sm">
            <MessageCircle className="size-6" aria-hidden="true" />
          </span>
          <div className="space-y-1">
            <h1 className="text-2xl font-semibold tracking-tight sm:text-3xl">
              {APP_NAME}
            </h1>
            <p className="text-sm text-muted-foreground">{APP_SUBTITLE}</p>
          </div>
        </header>

        <Card className="border-border/60 shadow-sm">
          <CardContent className="p-6 sm:p-8">{children}</CardContent> 
        </Card>
      </div>
    </div>
  );
}

export default AuthLayout;