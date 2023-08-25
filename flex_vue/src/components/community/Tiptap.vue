<template>
  <editor-content :editor="editor" />
</template>

<script setup>
import Starterkit from '@tiptap/starter-kit';
import { Editor, EditorContent } from '@tiptap/vue-3'
import { watch, onMounted, onBeforeUnmount, ref } from 'vue';

const props = defineProps({
  value: {
    type: String,
    default: '',
  },
});

let editor = null;

const isSame = ref(true);

watch(
  () => props.value,
  (newValue) => {
    // HTML
    isSame.value = editor.getHTML() === newValue;
    // JSON
    // isSame.value = JSON.stringify(editor.getJSON()) === JSON.stringify(newValue);

    if (!isSame.value) {
      editor.commands.setContent(newValue, false);
    }
  }
);

onMounted(() => {
  editor = new Editor({
    content: props.value,
    extensions: [Starterkit],
    onUpdate: () => {
      // HTML
      emit('input', editor.getHTML());
      // JSON
      // emit('input', editor.getJSON());
    },
  });
});

onBeforeUnmount(() => {
  editor.destroy();
});
</script>

