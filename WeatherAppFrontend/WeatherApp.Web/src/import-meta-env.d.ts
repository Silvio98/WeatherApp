interface ImportMetaEnv {
  readonly PROD: boolean;
  readonly DEV: boolean;
  // you can add more custom variables later if needed
}

interface ImportMeta {
  readonly env: ImportMetaEnv;
}
